using DG.Tweening;
using UnityEngine;

public class UIPauseController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public RectTransform soundButton, vibrateButton;

    private readonly float duration = 1f;

    public void OnEnable()
    {
        canvasGroup.interactable = true;
        canvasGroup.DOFade(0f, 0f).SetUpdate(true);
        canvasGroup.DOFade(1f, duration).SetUpdate(true);

        vibrateButton.DOAnchorPosX(290f, 0f).SetUpdate(true);
        soundButton.DOAnchorPosX(290f, 0f).SetUpdate(true);

        vibrateButton.DOAnchorPosX(120f, duration).SetEase(Ease.OutQuart).SetUpdate(true);
        soundButton.DOAnchorPosX(250f, duration).SetEase(Ease.OutQuart).SetUpdate(true);
    }

    public void Disable()
    {
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0f, duration / 2f).SetUpdate(true)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });

        vibrateButton.DOAnchorPosX(290f, duration / 2f).SetUpdate(true);
        soundButton.DOAnchorPosX(290f, duration / 2f).SetUpdate(true);
    }
}