using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIPlayController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        canvasGroup.interactable = true;
        canvasGroup.DOFade(0f, 0f).SetUpdate(true);
        canvasGroup.DOFade(1f, 1f).SetUpdate(true);
    }

    public void Disable()
    {
        canvasGroup.interactable = false;
        scoreText.transform.DOScale(Vector3.zero, 0f);
        canvasGroup.DOFade(0f, 0.5f).SetUpdate(true)
            .OnComplete(() =>
            {
                scoreText.transform.DOScale(Vector3.one, 0f);
                gameObject.SetActive(false);
            });

    }
}