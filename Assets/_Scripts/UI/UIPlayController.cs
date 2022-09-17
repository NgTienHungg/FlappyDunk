using DG.Tweening;
using UnityEngine;

public class UIPlayController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float fadeTime = 1f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        canvasGroup.DOFade(0f, 0f).SetUpdate(true);
        canvasGroup.DOFade(1f, fadeTime).SetUpdate(true);
    }
}