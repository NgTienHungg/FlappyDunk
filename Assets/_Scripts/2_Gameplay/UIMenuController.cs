using DG.Tweening;
using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject uiUnlockSkin;

    public RectTransform soundButton, vibrateButton;
    public RectTransform skinButton, challengeButton;
    public RectTransform adsButton, shareButton;

    private readonly float duration = 1f;

    private void OnEnable()
    {
        // setup
        vibrateButton.DOAnchorPosX(250f, 0f).SetUpdate(true);
        soundButton.DOAnchorPosX(250f, 0f).SetUpdate(true);

        adsButton.DOAnchorPosY(-300f, 0f).SetUpdate(true);
        shareButton.DOAnchorPosY(-300f, 0f).SetUpdate(true);

        skinButton.DOAnchorPosY(-300f, 0f).SetUpdate(true);
        challengeButton.DOAnchorPosY(-300f, 0f).SetUpdate(true);

        // anim
        vibrateButton.DOAnchorPosX(60, duration).SetEase(Ease.OutQuart).SetUpdate(true);
        soundButton.DOAnchorPosX(200f, duration).SetEase(Ease.OutQuart).SetUpdate(true);

        adsButton.DOAnchorPosY(-75f, duration).SetEase(Ease.OutQuart).SetUpdate(true);
        shareButton.DOAnchorPosY(-245f, duration).SetEase(Ease.OutQuart).SetUpdate(true);

        skinButton.DOAnchorPosY(-75f, duration).SetEase(Ease.OutQuart).SetUpdate(true);
        challengeButton.DOAnchorPosY(-245f, duration).SetEase(Ease.OutQuart).SetUpdate(true);

        uiUnlockSkin.SetActive(true);
    }
}