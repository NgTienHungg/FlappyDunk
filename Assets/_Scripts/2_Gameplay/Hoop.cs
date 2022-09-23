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
        Skin hoopSkin = GameManager.Instance.GetSkin(SkinType.Hoop, "HoopSelecting");
        if (GameManager.Instance.skinTryingType == SkinType.Hoop)
        {
            hoopSkin = GameManager.Instance.GetSkinTrying();
        }

        frontHoop.sprite = hoopSkin.profile.frontHoopSprite;
        backHoop.sprite = hoopSkin.profile.backHoopSprite;
        starParticle.textureSheetAnimation.SetSprite(0, hoopSkin.profile.starSprite);
    }

    public void Renew()
    {
        checkPoint.enabled = true;
        leftEdge.enabled = true;
        rightEdge.enabled = true;

        frontHoop.DOFade(0.5f, 0f).SetUpdate(true);
        backHoop.DOFade(0.5f, 0f).SetUpdate(true);
    }

    private void Awake()
    {
        this.Renew();
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

        // fade animation
        frontHoop.DOFade(0f, fadeDuration).SetEase(Ease.OutQuart);
        backHoop.DOFade(0f, fadeDuration).SetEase(Ease.OutQuart);

        // effect
        if (GameController.Instance.IsPerfect)
            starParticle.Play();
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