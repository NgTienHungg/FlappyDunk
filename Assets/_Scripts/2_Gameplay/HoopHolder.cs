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

    // setting
    private readonly float axisLength = 2f;
    private readonly float fadeDuration = 0.8f;
    private readonly float changeTargetDuration = 0.2f;

    public void Renew()
    {
        this.IsTargeting = false;
        this.canMove = false;

        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        this.hoop.Renew();
        this.hoop.transform.localPosition = Vector3.zero;

        this.axis.DOFade(0.5f, 0f).SetUpdate(true);
        this.axis.gameObject.SetActive(false);
    }

    public void LoadSkin()
    {
        this.hoop.LoadSkin();
    }

    private void Awake()
    {
        if (this.canMove)
            this.SetCanMove();
        else
            this.Renew();
    }

    private void Update()
    {
        if (this.canMove)
        {
            if (this.isMovingUp)
            {
                this.hoop.transform.Translate(Vector3.up * speed * Time.deltaTime);
                if (this.hoop.transform.position.y >= transform.position.y + this.rangeMovement)
                    this.isMovingUp = false;
            }
            else
            {
                this.hoop.transform.Translate(Vector3.down * speed * Time.deltaTime);
                if (this.hoop.transform.position.y <= transform.position.y - this.rangeMovement)
                    this.isMovingUp = true;
            }
        }
    }

    public void SetCanMove()
    {
        this.canMove = true;
        this.axis.gameObject.SetActive(true);

        this.isMovingUp = Random.Range(0, 2) == 1 ? true : false;
        this.angle = transform.eulerAngles.z * Mathf.Deg2Rad; // rad
        this.rangeMovement = Mathf.Cos(angle) * axisLength / 2f - 0.2f;
    }

    public void SetTarget()
    {
        this.IsTargeting = true;
        FindObjectOfType<Ball>().TargetHoopHolder = this;

        this.hoop.SetTarget(changeTargetDuration);
        if (this.canMove) this.axis.DOFade(1f, changeTargetDuration).SetEase(Ease.OutQuint).SetUpdate(true);
    }

    /* call when add score in game controller */
    public void ShowEffect()
    {
        this.IsTargeting = false;
        this.hoop.ShowEffect(fadeDuration);
        if (this.canMove) this.axis.DOFade(0f, fadeDuration).SetEase(Ease.OutQuart);

        this.transform.DOScale(Vector3.one * 2, fadeDuration).SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                this.Renew();
                gameObject.SetActive(false);
            });
    }

    public void Appear(float duration)
    {
        float targetAlpha = this.IsTargeting ? 1f : 0.5f;
        this.hoop.Appear(targetAlpha, duration);
        if (this.canMove) this.axis.DOFade(targetAlpha, duration).SetUpdate(true);
    }

    public void Fade(float duration)
    {
        this.hoop.Fade(duration);
        if (this.canMove) this.axis.DOFade(0f, duration).SetUpdate(true);
    }
}