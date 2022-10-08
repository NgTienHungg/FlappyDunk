using DG.Tweening;
using UnityEngine;

public class HoopHolder : MonoBehaviour
{
    [SerializeField] private Hoop hoop;
    [SerializeField] private SpriteRenderer axis;
    [SerializeField] private bool canMove;
    [SerializeField] private float speed;

    public bool IsTargeting { get; private set; }
    private float angle, rangeMovement;
    private bool isMovingUp;

    private readonly float axisLength = 2f;
    private readonly float fadeDuration = 0.8f;
    private readonly float changeTargetDuration = 0.25f;

    public void Renew()
    {
        this.IsTargeting = false;
        this.canMove = false;

        transform.rotation = Quaternion.identity;

        hoop.Renew();

        axis.DOFade(0.5f, 0f).SetUpdate(true);
        axis.gameObject.SetActive(false);
    }

    public void LoadSkin()
    {
        hoop.LoadSkin();
    }

    private void Awake()
    {
        this.IsTargeting = false;

        hoop.Renew();
        hoop.transform.localPosition = Vector3.zero;

        axis.DOFade(0.5f, 0f).SetUpdate(true);
        axis.gameObject.SetActive(false);

        if (this.canMove) this.SetCanMove(); // set up hoop trong các Challenge
    }

    private void Update()
    {
        if (this.canMove)
        {
            if (this.isMovingUp)
            {
                hoop.transform.Translate(Vector3.up * speed * Time.deltaTime);

                if (this.hoop.transform.position.y >= transform.position.y + this.rangeMovement)
                    this.isMovingUp = false;
            }
            else
            {
                hoop.transform.Translate(Vector3.down * speed * Time.deltaTime);

                if (this.hoop.transform.position.y <= transform.position.y - this.rangeMovement)
                    this.isMovingUp = true;
            }
        }
    }

    public void SetCanMove()
    {
        axis.gameObject.SetActive(true);

        this.canMove = true;
        this.isMovingUp = Random.Range(0, 2) == 1 ? true : false;
        this.angle = transform.eulerAngles.z * Mathf.Deg2Rad; // rad
        this.rangeMovement = Mathf.Cos(angle) * axisLength / 2f - 0.2f;
    }

    public void SetTarget()
    {
        FindObjectOfType<Ball>().TargetHoopHolder = this;

        this.IsTargeting = true;

        hoop.SetTarget(changeTargetDuration);

        if (this.canMove)
            axis.DOFade(1f, changeTargetDuration).SetEase(Ease.OutQuint).SetUpdate(true);
    }

    public void ShowEffect()
    {
        this.IsTargeting = false;

        hoop.ShowEffect(fadeDuration);

        axis.DOFade(0f, fadeDuration)
            .OnComplete(() =>
            {
                this.Renew();
                gameObject.SetActive(false);
            });
    }

    public void Appear(float duration)
    {
        float targetAlpha = this.IsTargeting ? 1f : 0.5f;

        hoop.Appear(targetAlpha, duration);

        if (this.canMove)
            axis.DOFade(targetAlpha, duration).SetUpdate(true);
    }

    public void Fade(float duration)
    {
        hoop.Fade(duration);

        if (this.canMove)
            axis.DOFade(0f, duration).SetUpdate(true);
    }
}