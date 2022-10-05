using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    // components
    private Rigidbody2D rigidBody;
    private Animator animator;
    private BallSkin ballSkin;
    private Wings wings;

    [SerializeField]
    private float horizontalForce, verticalForce;
    private int collisionWithFloor;

    public bool IsAlive { get; set; }
    public Hoop TargetHoop { get; set; }
    public HoopHolder TargetHoopHolder { get; set; }

    // physic setting
    private readonly float limitHorizontalVelocity = 2.2f;
    private readonly float limitAngularVelocity = 400f;
    private readonly int limitCollisionWithFloor = 7;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ballSkin = GetComponent<BallSkin>();
        wings = GetComponent<Wings>();
    }

    private void OnEnable()
    {
        MyEvent.OnCompleteChallenge += Congratulate;
    }

    private void OnDisable()
    {
        MyEvent.OnCompleteChallenge -= Congratulate;
    }

    private void Start()
    {
        this.IsAlive = true;
        this.collisionWithFloor = 0;

        if (GameManager.Instance.gameMode == GameMode.Challenge)
        {
            Challenge challenge = GameManager.Instance.challenges[PlayerPrefs.GetInt("ChallengePlaying") - 1];
            if (challenge.profile.type == ChallengeType.StrongWing)
            {
                verticalForce = challenge.profile.flapForceY;
            }
        }
    }

    private void Update()
    {
        if (!GameController.Instance.IsPlaying || !this.IsAlive)
            return;

        if (Input.GetMouseButtonDown(0) && !Util.IsPointerOverUIObject())
            this.Flap();

        this.CheckTargetHoop();
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
        if (TargetHoop == null)
            return;

        // nếu bóng ở dưới vòng thì cho chết sớm hơn so với khi ở trên vòng
        if (this.transform.position.y < TargetHoop.transform.position.y)
        {
            if (this.transform.position.x > TargetHoop.transform.position.x + 0.5)
                this.Dead();
        }
        else if (this.transform.position.x >= TargetHoop.transform.position.x + 1f)
        {
            this.Dead();
        }
    }

    #region ACTION
    public void Fade(float fadeDuration)
    {
        ballSkin.Fade(fadeDuration);
    }

    public void Appear(float appearDuration)
    {
        ballSkin.Appear(appearDuration);
    }

    private void Flap()
    {
        animator.Play("Flap", 0, 0);

        AudioManager.Instance.PlaySound("Flap");

        rigidBody.velocity = Vector2.zero;
        rigidBody.AddForce(new Vector3(horizontalForce, verticalForce));
    }

    private void Explode()
    {
        wings.Explode();
        AudioManager.Instance.PlaySound("Crash");
    }

    private void Dead()
    {
        AudioManager.Instance.PlaySound("Wrong");
        MyEvent.BallNormal?.Invoke();
        MyEvent.BallDead?.Invoke();
        this.IsAlive = false;
    }

    public void Revive()
    {
        this.IsAlive = true;
        collisionWithFloor = 0;
        wings.Renew();

        transform.position = new Vector3(TargetHoop.transform.position.x - 3f, 0f);
        transform.rotation = Quaternion.identity;

        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
    }

    public void Congratulate()
    {
        horizontalForce = 120f;
        verticalForce = 350f;
        StartCoroutine(PassChallenge());
    }

    private IEnumerator PassChallenge()
    {
        // bay lên giữa màn hình
        while (transform.position.y < 0f)
        {
            animator.Play("Flap", 0, 0);

            AudioManager.Instance.PlaySound("Flap");

            rigidBody.velocity = new Vector2(0f, 0f);
            rigidBody.AddForce(new Vector3(80f, 300f));

            yield return new WaitForSeconds(0.3f);
        }

        // chờ bóng rơi xuống giữa màn hình
        while (transform.position.y > 0f)
            yield return null;

        Camera.main.GetComponent<CameraFollowBall>().UnFollowBall();

        for (int i = 0; i < 4; i++)
        {
            if (transform.position.x > Camera.main.transform.position.x + 0.8f)
                GameController.Instance.OnBackHome();

            this.Flap();

            yield return new WaitForSeconds(0.75f);
        }
    }
    #endregion


    #region PHYSIC
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            if (collisionWithFloor == 0)
                this.Explode();

            // fix bug infinite bouncing ball
            if (collisionWithFloor < limitCollisionWithFloor)
            {
                AudioManager.Instance.PlaySound("Bounce");

                collisionWithFloor++;
                if (collisionWithFloor >= limitCollisionWithFloor)
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            }
        }

        if (!this.IsAlive)
            return;

        if (collision.gameObject.CompareTag("Platform"))
        {
            AudioManager.Instance.PlaySound("Bounce");
            this.Dead();
        }

        if (collision.gameObject.CompareTag("HoopEdge"))
        {
            // vận tốc tương đối giữa 2 vật thể
            if (collision.relativeVelocity.magnitude >= 1f)
                AudioManager.Instance.PlaySound("Bounce");

            ScoreManager.Instance.isPerfect = false;
            MyEvent.BallNormal?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.IsAlive)
            return;

        if (collision.gameObject.CompareTag("Hoop"))
        {
            if (collision.transform.eulerAngles.z == 90f)
                return;

            if (rigidBody.velocity.y > 0f)
                this.Dead();
        }
        else if (collision.gameObject.CompareTag("FinishLine"))
        {
            GameManager.Instance.ChallengePlaying.Pass();
            MyEvent.OnCompleteChallenge?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!this.IsAlive)
            return;

        if (collision.gameObject.CompareTag("Hoop"))
        {
            if (collision.transform.eulerAngles.z == 90f)
            {
                if (transform.position.x < collision.transform.position.x)
                    return;
            }
            else
            {
                if (rigidBody.velocity.y > 0f)
                    return;
            }

            //! not change
            AudioManager.Instance.PlaySound("Pass");
            this.TargetHoopHolder.ShowEffect();
            MyEvent.PassHoop?.Invoke();
        }
    }
    #endregion
}