using DG.Tweening;
using UnityEngine;

public class UIPauseController : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    private readonly float duration = 0.6f;

    public void OnEnable()
    {
        canvasGroup.interactable = true;
        canvasGroup.DOFade(0f, 0f).SetUpdate(true);
        canvasGroup.DOFade(1f, duration).SetUpdate(true);
    }

    public void Disable()
    {
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0f, duration / 2f).SetUpdate(true)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}