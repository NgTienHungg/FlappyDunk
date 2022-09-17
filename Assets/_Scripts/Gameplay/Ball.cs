using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // skin
    private BallSkin ballSkin;
    private WingSkin wingSkin;
    private FlameSkin flameSkin;

    // component
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private Animator animator;

    // wing
    [SerializeField] private SpriteRenderer srFrontWing, srBackWing;
    [SerializeField] private Rigidbody2D rbFrontWing, rbBackWing;
    private Vector3 frontWingPosition, backWingPosition;

    // other
    [SerializeField] private ParticleSystem psSmoke, psFlame;
    [SerializeField] private float horizontalForce, verticalForce;
    private int collisionWithFloor;

    public bool IsAlive { get; private set; }
    public Hoop TargetHoop { get; set; }
    public HoopHolder TargetHoopHolder { get; set; }

    // physic setting
    private readonly float limitHorizontalVelocity = 2f;
    private readonly float limitAngularVelocity = 400f;
    private readonly int limitCollisionWithFloor = 6;


    public void LoadSkin()
    {
        BallSkin ballSkin;
        WingSkin wingSkin;
        FlameSkin flameSkin;

        // load data
        if (GameManager.Instance.IsTrying && GameManager.Instance.tryCode == "Ball")
            ballSkin = GameManager.Instance.dataBall.ballSkins[GameManager.Instance.tryID];
        else
            ballSkin = GameManager.Instance.dataBall.ballSkins[PlayerPrefs.GetInt("BallSelecting")];

        if (GameManager.Instance.IsTrying && GameManager.Instance.tryCode == "Wing")
            wingSkin = GameManager.Instance.dataWing.wingSkins[GameManager.Instance.tryID];
        else
            wingSkin = GameManager.Instance.dataWing.wingSkins[PlayerPrefs.GetInt("WingSelecting")];

        if (GameManager.Instance.IsTrying && GameManager.Instance.tryCode == "Flame")
            flameSkin = GameManager.Instance.dataFlame.flameSkins[GameManager.Instance.tryID];
        else
            flameSkin = GameManager.Instance.dataFlame.flameSkins[PlayerPrefs.GetInt("FlameSelecting")];

        // load skin
        spriteRenderer.sprite = ballSkin.sprite;
        srFrontWing.sprite = wingSkin.sprite;
        srBackWing.sprite = wingSkin.sprite;

        ParticleSystem.MainModule psMain = psFlame.main;
        psMain.startColor = flameSkin.color;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        frontWingPosition = srFrontWing.transform.localPosition;
        backWingPosition = srBackWing.transform.localPosition;

        rbFrontWing.bodyType = RigidbodyType2D.Kinematic;
        rbBackWing.bodyType = RigidbodyType2D.Kinematic;

        this.IsAlive = true;
        collisionWithFloor = 0;
    }

    /* wait GameController.Awake */
    private void Start()
    {
        this.LoadSkin();
    }

    private void Update()
    {
        if (!GameController.Instance.IsPlaying || !this.IsAlive)
            return;

        this.CheckTargetHoop();

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !Util.IsPointerOverUIObject())
            this.Flap();
    }

    private void FixedUpdate()
    {
        if (rigidBody.velocity.x >= limitHorizontalVelocity)
            rigidBody.velocity = new Vector2(limitHorizontalVelocity, rigidBody.velocity.y);

        if (Mathf.Abs(rigidBody.angularVelocity) >= limitAngularVelocity)
            rigidBody.angularVelocity = Mathf.Sign(rigidBody.angularVelocity) * limitAngularVelocity;
    }

    private void CheckTargetHoop()
    {
        // neu ball o duoi qua bong thi cho chet som hon
        if (this.transform.position.y < TargetHoop.transform.position.y)
        {
            if (this.transform.position.x > TargetHoop.transform.position.x + 0.25f)
                this.Dead();
        }
        else if (this.transform.position.x >= TargetHoop.transform.position.x + 1f)
            this.Dead();
    }

    public void UpdateState(int combo)
    {
        // call when ball get swish
        if (combo == 1)
            this.NormalMode();
        else if (combo == 2)
        {
            this.FumingMode();
            rigidBody.AddForce(new Vector2(0f, -10f));
            AudioManager.Instance.PlaySound("SwishX2");
        }
        else if (combo >= 3)
        {
            this.FlamingMode();
            rigidBody.AddForce(new Vector2(0f, -20f));

            if (combo == 3)
                AudioManager.Instance.PlaySound("SwishX3");
            else
                AudioManager.Instance.PlaySound("SwishX4");
        }
    }


    #region ANIMATION
    public void Fade(float fadeDuration)
    {
        spriteRenderer.DOFade(0f, fadeDuration).SetUpdate(true);
        srFrontWing.DOFade(0f, fadeDuration).SetUpdate(true);
        srBackWing.DOFade(0f, fadeDuration).SetUpdate(true);
    }

    public void Appear(float appearDuration)
    {
        spriteRenderer.DOFade(1f, appearDuration).SetUpdate(true);
        srFrontWing.DOFade(1f, appearDuration).SetUpdate(true);
        srBackWing.DOFade(1f, appearDuration).SetUpdate(true);
    }

    private void NormalMode()
    {
        psSmoke.Stop();
        psFlame.Stop();
    }

    private void FumingMode()
    {
        psSmoke.Play();
        psFlame.Stop();
    }

    private void FlamingMode()
    {
        psSmoke.Stop();
        psFlame.Play();
    }
    #endregion


    #region ACTION
    private void Flap()
    {
        AudioManager.Instance.PlaySound("Flap");
        animator.Play("Flap", 0, 0);

        rigidBody.velocity = new Vector2(0f, 0f);
        rigidBody.AddForce(new Vector3(horizontalForce, verticalForce), ForceMode2D.Force);
    }

    private void Crash()
    {
        AudioManager.Instance.PlaySound("Crash");

        rbFrontWing.bodyType = RigidbodyType2D.Dynamic;
        rbBackWing.bodyType = RigidbodyType2D.Dynamic;

        rbFrontWing.AddForce(new Vector2(Random.Range(-50f, -20f), Random.Range(300f, 400f)));
        rbBackWing.AddForce(new Vector2(Random.Range(100f, 120f), Random.Range(300f, 400f)));
    }

    private void Dead()
    {
        AudioManager.Instance.PlaySound("Wrong");

        this.IsAlive = false;
        this.NormalMode();
    }

    public void Revive()
    {
        this.IsAlive = true;

        transform.position = new Vector3(TargetHoop.transform.position.x - 3f, 0f);
        transform.rotation = Quaternion.identity;

        // wings
        srFrontWing.transform.localPosition = frontWingPosition;
        srBackWing.transform.localPosition = backWingPosition;

        rbFrontWing.bodyType = RigidbodyType2D.Kinematic;
        rbBackWing.bodyType = RigidbodyType2D.Kinematic;

        rbFrontWing.velocity = Vector2.zero;
        rbBackWing.velocity = Vector2.zero;

        rbFrontWing.angularVelocity = 0f;
        rbBackWing.angularVelocity = 0f;

        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;

        collisionWithFloor = 0;
    }
    #endregion


    #region PHYSIC
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Instance.PlaySound("Bounce");

        if (collision.gameObject.name == "Floor")
        {
            if (collisionWithFloor == 0)
                this.Crash();

            // fix bug infinite bouncing ball
            if (collisionWithFloor < limitCollisionWithFloor)
            {
                collisionWithFloor++;
                if (collisionWithFloor >= limitCollisionWithFloor)
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            }
        }

        if (!this.IsAlive)
            return;

        if (collision.gameObject.CompareTag("Platform"))
            this.Dead();

        if (collision.gameObject.CompareTag("HoopEdge"))
        {
            this.NormalMode();
            GameController.Instance.IsPerfect = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.IsAlive)
            return;

        if (collision.gameObject.CompareTag("Hoop"))
            if (transform.position.y < collision.gameObject.transform.position.y)
                this.Dead();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!this.IsAlive)
            return;

        if (collision.gameObject.CompareTag("Hoop"))
        {
            if (transform.position.y < collision.gameObject.transform.position.y)
            {
                AudioManager.Instance.PlaySound("Pass");

                //! not change
                this.TargetHoopHolder.ShowEffect();
                GameController.Instance.AddScore();
            }
        }
    }
    #endregion
}