using UnityEngine;
using DG.Tweening;

public class Hoop : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private SpriteRenderer frontHoop;
    [SerializeField] private SpriteRenderer backHoop;
    [SerializeField] private CapsuleCollider2D leftEdge, rightEdge;
    [SerializeField] private BoxCollider2D checkPoint;

    [Header("Effect")]
    [SerializeField] private ParticleSystem startEffect;
    [SerializeField] private ParticleSystem blastEffect;
    [SerializeField] private ParticleSystem smokeEffect;

    private readonly Vector3 normalScale = new Vector3(1f, 0.6f);
    private readonly Vector3 bigScale = new Vector3(2f, 1.2f);

    public void LoadSkin()
    {
        Skin hoopSkin = GameManager.Instance.GetSkinSelecting(SkinType.Hoop);
        Skin flameSkin = GameManager.Instance.GetSkinSelecting(SkinType.Flame);

        if (GameManager.Instance.gameMode == GameMode.Trying)
        {
            if (GameManager.Instance.skinTypeTrying == SkinType.Hoop)
                hoopSkin = GameManager.Instance.GetSkinTrying();
            else if (GameManager.Instance.skinTypeTrying == SkinType.Flame)
                flameSkin = GameManager.Instance.GetSkinTrying();
        }

        frontHoop.sprite = hoopSkin.profile.frontHoopSprite;
        backHoop.sprite = hoopSkin.profile.backHoopSprite;
        startEffect.textureSheetAnimation.SetSprite(0, hoopSkin.profile.starSprite);

        ParticleSystem.MainModule mainBlast = blastEffect.main;
        mainBlast.startColor = flameSkin.profile.flameColor;

        ParticleSystem.MainModule mainSmoke = smokeEffect.main;
        mainSmoke.startColor = flameSkin.profile.flameColor;
    }

    private void Awake()
    {
        this.Renew();
    }

    public void Renew()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = normalScale;

        checkPoint.enabled = true;
        leftEdge.enabled = true;
        rightEdge.enabled = true;

        frontHoop.DOFade(0.5f, 0f).SetUpdate(true);
        backHoop.DOFade(0.5f, 0f).SetUpdate(true);
    }

    public void SetTarget(float duration)
    {
        FindObjectOfType<Ball>().TargetHoop = this;

        frontHoop.DOFade(1f, duration).SetEase(Ease.OutQuint).SetUpdate(true);
        backHoop.DOFade(1f, duration).SetEase(Ease.OutQuint).SetUpdate(true);
    }

    public void ShowEffect(float fadeDuration)
    {
        // disable collider
        checkPoint.enabled = false;
        leftEdge.enabled = false;
        rightEdge.enabled = false;

        // animation
        transform.DOScale(bigScale, fadeDuration).SetEase(Ease.OutQuart);
        frontHoop.DOFade(0f, fadeDuration).SetEase(Ease.OutQuart);
        backHoop.DOFade(0f, fadeDuration).SetEase(Ease.OutQuart);

        // effect
        if (ScoreManager.Instance.isPerfect)
        {
            startEffect.Play();

            if (ScoreManager.Instance.combo >= 2)
            {
                blastEffect.Play();
                smokeEffect.Play();
            }
        }
    }

    public void Appear(float target, float duration)
    {
        frontHoop.DOFade(target, duration).SetUpdate(true);
        backHoop.DOFade(target, duration).SetUpdate(true);
    }

    public void Fade(float duration)
    {
        frontHoop.DOFade(0f, duration).SetUpdate(true);
        backHoop.DOFade(0f, duration).SetUpdate(true);
    }
}