using DG.Tweening;
using UnityEngine;

public class HoopHolder : MonoBehaviour
{
    [SerializeField] private Hoop hoop;
    [SerializeField] private SpriteRenderer axis;
    [SerializeField] private float speed;

    public bool IsTargeting { get; private set; }
    public bool CanMove { get; private set; }
    private float angle, rangeMovement;
    private bool isMovingUp;

    // setting
    private readonly float axisLength = 1.8f;
    private readonly float fadeDuration = 0.8f;
    private readonly float changeTargetDuration = 0.2f;



    public void Renew()
    {
        this.IsTargeting = false;
        this.CanMove = false;
        this.isMovingUp = false;
        this.axis.color = Color.clear;
        this.hoop.Renew();
        transform.localScale = Vector3.one;
    }

    private void Awake() => this.Renew();

    private void Update()
    {
        if (this.CanMove)
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
        this.CanMove = true;
        this.isMovingUp = Random.Range(0, 2) == 1 ? true : false;
        this.axis.color = new Color(1f, 1f, 1f, 0.5f);
        this.angle = transform.eulerAngles.z * Mathf.Deg2Rad; // rad
        this.rangeMovement = Mathf.Cos(angle) * axisLength / 2f - 0.2f;
    }

    public void SetTarget()
    {
        this.IsTargeting = true;
        this.hoop.SetTarget(changeTargetDuration);
        GameController.Instance.ball.TargetHoopHolder = this;
        if (this.CanMove) this.axis.DOFade(1f, changeTargetDuration).SetEase(Ease.OutQuint).SetUpdate(true);
    }

    /* call when add score */
    public void ShowEffect()
    {
        this.IsTargeting = false;
        this.hoop.ShowEffect(fadeDuration);
        if (this.CanMove) this.axis.DOFade(0f, fadeDuration).SetEase(Ease.OutQuart);

        this.transform.DOScale(Vector3.one * 2, fadeDuration).SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                this.Renew();
                this.gameObject.SetActive(false);
            });
    }

    public void Appear(float appearDuration)
    {
        float targetAlpha = this.IsTargeting ? 1f : 0.5f;
        this.hoop.Appear(targetAlpha, appearDuration);
        if (this.CanMove) this.axis.DOFade(targetAlpha, appearDuration).SetUpdate(true);
    }

    public void Fade(float fadeDuration)
    {
        this.hoop.Fade(fadeDuration);
        if (this.CanMove) this.axis.DOFade(0f, fadeDuration).SetUpdate(true);
    }
}