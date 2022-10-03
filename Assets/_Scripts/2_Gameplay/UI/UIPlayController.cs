using UnityEngine;
using DG.Tweening;

public class UIPlayController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private readonly float fadeDuration = 1f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        canvasGroup.DOFade(0f, 0f).SetUpdate(true);
        canvasGroup.DOFade(1f, fadeDuration).SetUpdate(true);
    }
}