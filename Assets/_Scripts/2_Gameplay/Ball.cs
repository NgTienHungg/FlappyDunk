using UnityEngine;
using System.Collections;

public enum BallStatus
{
    Normal,
    Fuming,
    Flaming
}

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private BallSkin ballSkin;
    private BallStatus status;

    public Rigidbody2D frontWingRigidBody, backWingRigidBody;
    public float horizontalForce, verticalForce;
    private int collisionWithFloor;

    public bool IsAlive { get; private set; }
    public Hoop TargetHoop { get; set; }
    public HoopHolder TargetHoopHolder { get; set; }

    // physic setting
    private readonly float limitHorizontalVelocity = 2.2f;
    private readonly float limitAngularVelocity = 400f;
    private readonly int limitCollisionWithFloor = 8;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ballSkin = GetComponent<BallSkin>();

        frontWingRigidBody.bodyType = RigidbodyType2D.Kinematic;
        backWingRigidBody.bodyType = RigidbodyType2D.Kinematic;

        this.IsAlive = true;
        this.status = BallStatus.Normal;
        this.collisionWithFloor = 0;
    }

    private void Update()
    {
        if (!GameController.Instance.IsPlaying || !this.IsAlive)
            return;

        this.CheckTargetHoop();

        if (Input.GetMouseButtonDown(0) && !Util.IsPointerOverUIObject())
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
        if (TargetHoop == null)
            return;

        // neu ball o duoi qua bong thi cho chet som hon
        if (this.transform.position.y < TargetHoop.transform.position.y)
        {
            if (this.transform.position.x > TargetHoop.transform.position.x + 0.5)
                this.Dead();
        }
        else if (this.transform.position.x >= TargetHoop.transform.position.x + 1f)
            this.Dead();
    }

    public void UpdateState(int combo)
    {
        if (combo == 1)
        {
            if (this.status != BallStatus.Normal)
            {
                this.status = BallStatus.Normal;
                MyEvent.BallNormal?.Invoke();
            }
        }
        else if (combo == 2)
        {
            AudioManager.Instance.PlaySound("SwishX2");
            this.status = BallStatus.Fuming;
            MyEvent.BallFuming?.Invoke();
        }
        else if (combo == 3)
        {
            AudioManager.Instance.PlaySound("SwishX3");
            this.status = BallStatus.Flaming;
            MyEvent.BallFlaming?.Invoke();
        }
        else if (combo >= 4)
        {
            AudioManager.Instance.PlaySound("SwishX4");
        }
    }


    #region ANIMATION
    public void Fade(float fadeDuration)
    {
        ballSkin.Fade(fadeDuration);
    }

    public void Appear(float appearDuration)
    {
        ballSkin.Appear(appearDuration);
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

        frontWingRigidBody.bodyType = RigidbodyType2D.Dynamic;
        backWingRigidBody.bodyType = RigidbodyType2D.Dynamic;

        frontWingRigidBody.AddForce(new Vector2(Random.Range(-50f, -20f), Random.Range(300f, 400f)));
        backWingRigidBody.AddForce(new Vector2(Random.Range(100f, 120f), Random.Range(300f, 400f)));
    }

    private void Dead()
    {
        AudioManager.Instance.PlaySound("Wrong");

        this.IsAlive = false;
        if (this.status != BallStatus.Normal)
        {
            this.status = BallStatus.Normal;
            MyEvent.BallNormal?.Invoke();
        }
    }

    public void Revive()
    {
        this.IsAlive = true;
        this.status = BallStatus.Normal;

        transform.position = new Vector3(TargetHoop.transform.position.x - 3f, 0f);
        transform.rotation = Quaternion.identity;

        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;

        ballSkin.ResetWing();

        frontWingRigidBody.bodyType = RigidbodyType2D.Kinematic;
        backWingRigidBody.bodyType = RigidbodyType2D.Kinematic;

        frontWingRigidBody.velocity = Vector2.zero;
        backWingRigidBody.velocity = Vector2.zero;

        frontWingRigidBody.angularVelocity = 0f;
        backWingRigidBody.angularVelocity = 0f;

        collisionWithFloor = 0;
    }

    private IEnumerator PassChallenge()
    {
        MyEvent.OnCompleteChallenge?.Invoke();
        GameManager.Instance.gameMode = GameMode.Endless;

        // bay len giua man hinh
        while (transform.position.y < 0f)
        {
            AudioManager.Instance.PlaySound("Flap");
            animator.Play("Flap", 0, 0);
            rigidBody.velocity = new Vector2(0f, 0f);
            rigidBody.AddForce(new Vector3(80f, 300f));
            yield return new WaitForSeconds(0.3f);
        }

        while (transform.position.y > 0f)
            yield return null;

        Camera.main.GetComponent<CameraFollowBall>().UnFollowBall();

        for (int i = 0; i < 4; i++)
        {
            if (transform.position.x > Camera.main.transform.position.x + 1)
                GameController.Instance.OnBackHome();

            AudioManager.Instance.PlaySound("Flap");
            animator.Play("Flap", 0, 0);
            rigidBody.velocity = new Vector2(0f, 0f);
            rigidBody.AddForce(new Vector3(100f, 350f));
            yield return new WaitForSeconds(0.75f);
        }
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
            GameController.Instance.IsPerfect = false;
            if (this.status != BallStatus.Normal)
            {
                this.status = BallStatus.Normal;
                MyEvent.BallNormal?.Invoke();
            }
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
            GameManager.Instance.challenges[PlayerPrefs.GetInt("ChallengePlaying") - 1].Pass();
            GameController.Instance.IsPlaying = false;
            StartCoroutine(PassChallenge());
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
            GameController.Instance.AddScore();
        }
    }
    #endregion
}