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

    public void LoadSkin()
    {
        Skin hoopSkin = GameManager.Instance.GetSkin(SkinType.Hoop, "HoopSelecting");
        Skin flameSkin = GameManager.Instance.GetSkin(SkinType.Flame, "FlameSelecting");

        if (GameManager.Instance.gameMode == GameMode.Trying)
        {
            if (GameManager.Instance.skinTryingType == SkinType.Hoop)
                hoopSkin = GameManager.Instance.GetSkinTrying();
            else if (GameManager.Instance.skinTryingType == SkinType.Flame)
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
        {
            startEffect.Play();

            if (GameController.Instance.Combo >= 2)
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