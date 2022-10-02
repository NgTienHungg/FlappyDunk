using DG.Tweening;
using UnityEngine;

public class BallSkin : MonoBehaviour
{
    [Header("Normal")]
    [SerializeField] private SpriteRenderer normalBall;
    [SerializeField] private SpriteRenderer normalFrontWing;
    [SerializeField] private SpriteRenderer normalBackWing;

    [Header("Fever")]
    [SerializeField] private SpriteRenderer feverBall;
    [SerializeField] private SpriteRenderer feverFrontWing;
    [SerializeField] private SpriteRenderer feverBackWing;

    [Header("Effect")]
    [SerializeField] private ParticleSystem smokeEffect;
    [SerializeField] private ParticleSystem flameEffect;

    private Skin ballSkin, wingSkin, flameSkin;

    private readonly float feverFadeDuration = 0.4f;

    private void OnEnable()
    {
        MyEvent.BallNormal += Normal;
        MyEvent.BallFuming += Fuming;
        MyEvent.BallFlaming += Flaming;
    }

    private void OnDisable()
    {
        MyEvent.BallNormal -= Normal;
        MyEvent.BallFlaming -= Fuming;
        MyEvent.BallFlaming -= Flaming;
    }

    private void Start()
    {
        this.LoadData();
        this.LoadSkin();
        this.SetUp();
    }

    private void LoadData()
    {
        ballSkin = GameManager.Instance.GetSkin(SkinType.Ball, "BallSelecting");
        wingSkin = GameManager.Instance.GetSkin(SkinType.Wing, "WingSelecting");
        flameSkin = GameManager.Instance.GetSkin(SkinType.Flame, "FlameSelecting");

        if (GameManager.Instance.gameMode == GameMode.Trying)
        {
            Skin skinTrying = GameManager.Instance.GetSkinTrying();
            SkinType skinTypeTrying = GameManager.Instance.skinTypeTrying;

            switch (skinTypeTrying)
            {
                case SkinType.Ball:
                    ballSkin = skinTrying;
                    break;
                case SkinType.Wing:
                    wingSkin = skinTrying;
                    break;
                case SkinType.Flame:
                    flameSkin = skinTrying;
                    break;
            }
        }
    }

    private void LoadSkin()
    {
        normalBall.sprite = ballSkin.profile.ballSprite;
        normalFrontWing.sprite = wingSkin.profile.wingSprite;
        normalBackWing.sprite = wingSkin.profile.wingSprite;

        ParticleSystem.MainModule mainFlame = flameEffect.main;
        mainFlame.startColor = flameSkin.profile.flameColor;

        feverBall.color = flameSkin.profile.flameColor;
        feverFrontWing.color = flameSkin.profile.flameColor;
        feverBackWing.color = flameSkin.profile.flameColor;
    }

    private void SetUp()
    {
        feverBall.DOFade(0f, 0f).SetUpdate(true);
        feverFrontWing.DOFade(0f, 0f).SetUpdate(true);
        feverBackWing.DOFade(0f, 0f).SetUpdate(true);

        smokeEffect.Stop();
        flameEffect.Stop();
    }

    private void Normal()
    {
        feverBall.DOFade(0f, feverFadeDuration);
        feverFrontWing.DOFade(0f, feverFadeDuration);
        feverBackWing.DOFade(0f, feverFadeDuration);

        smokeEffect.Stop();
        flameEffect.Stop();
    }

    private void Fuming()
    {
        try
        {
            smokeEffect.Play();
        }
        catch (MissingReferenceException ex)
        {
            Debug.Log(ex);
            smokeEffect = Instantiate(GameManager.Instance.smokeEffectPrefab, transform);
            smokeEffect.Play();
        }
    }

    private void Flaming()
    {
        feverBall.DOFade(1f, feverFadeDuration);
        feverFrontWing.DOFade(1f, feverFadeDuration);
        feverBackWing.DOFade(1f, feverFadeDuration);

        smokeEffect.Stop();
        flameEffect.Play();
    }

    public void Appear(float duration)
    {
        normalBall.DOFade(1f, duration).SetUpdate(true);
        normalFrontWing.DOFade(1f, duration).SetUpdate(true);
        normalBackWing.DOFade(1f, duration).SetUpdate(true);
    }

    public void Fade(float duration)
    {
        normalBall.DOFade(0f, duration).SetUpdate(true);
        normalFrontWing.DOFade(0f, duration).SetUpdate(true);
        normalBackWing.DOFade(0f, duration).SetUpdate(true);
    }
}