using UnityEngine;
using DG.Tweening;

public class Hoop : MonoBehaviour
{
    [SerializeField] private SpriteRenderer frontHoop, backHoop;
    [SerializeField] private CapsuleCollider2D leftEdge, rightEdge;
    [SerializeField] private BoxCollider2D checkPoint;
    [SerializeField] private ParticleSystem starParticle;

    public void LoadSkin()
    {
        HoopSkin hoopSkin;
        if (GameManager.Instance.IsTrying && GameManager.Instance.tryCode == "Hoop")
            hoopSkin = GameManager.Instance.dataHoop.hoopSkins[GameManager.Instance.tryID];
        else
            hoopSkin = GameManager.Instance.dataHoop.hoopSkins[PlayerPrefs.GetInt("HoopSelecting")];

        frontHoop.sprite = hoopSkin.frontHoop;
        backHoop.sprite = hoopSkin.backHoop;
        starParticle.textureSheetAnimation.SetSprite(0, hoopSkin.star);
    }

    public void Renew()
    {
        this.checkPoint.enabled = true;
        this.leftEdge.enabled = true;
        this.rightEdge.enabled = true;

        this.frontHoop.DOFade(0.5f, 0f).SetUpdate(true);
        this.backHoop.DOFade(0.5f, 0f).SetUpdate(true);
    }

    private void Awake()
    {
        this.Renew();
    }

    public void SetTarget(float changeTargetDuration)
    {
        // set target hoop for ball
        GameController.Instance.ball.TargetHoop = this;

        this.frontHoop.DOFade(1f, changeTargetDuration).SetEase(Ease.OutQuint).SetUpdate(true);
        this.backHoop.DOFade(1f, changeTargetDuration).SetEase(Ease.OutQuint).SetUpdate(true);
    }

    public void ShowEffect(float fadeDuration)
    {
        // disable collider
        this.checkPoint.enabled = false;
        this.leftEdge.enabled = false;
        this.rightEdge.enabled = false;

        // fade animation
        this.frontHoop.DOFade(0f, fadeDuration).SetEase(Ease.OutQuart);
        this.backHoop.DOFade(0f, fadeDuration).SetEase(Ease.OutQuart);


        // effect
        if (GameController.Instance.IsPerfect)
            this.starParticle.Play();
    }

    public void Appear(float targetAlpha, float appearDuration)
    {
        frontHoop.DOFade(targetAlpha, appearDuration).SetUpdate(true);
        backHoop.DOFade(targetAlpha, appearDuration).SetUpdate(true);
    }

    public void Fade(float fadeDuration)
    {
        frontHoop.DOFade(0f, fadeDuration).SetUpdate(true);
        backHoop.DOFade(0f, fadeDuration).SetUpdate(true);
    }
}