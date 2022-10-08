using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIPlayController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI scoreText;
    private readonly float duration = 1f;

    private void OnEnable()
    {
        canvasGroup.interactable = true;
        canvasGroup.DOFade(0f, 0f).SetUpdate(true);
        canvasGroup.DOFade(1f, duration).SetUpdate(true);

        if (GameManager.Instance.gameMode == GameMode.Challenge)
            scoreText.gameObject.SetActive(false);
        else
            scoreText.gameObject.SetActive(true);
    }

    public void Disable()
    {
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0f, duration / 2f).SetUpdate(true)
            .OnComplete(() =>
            {
                scoreText.transform.DOScale(Vector3.one, 0f);
                gameObject.SetActive(false);
            });

        scoreText.transform.DOScale(Vector3.zero, 0f);
    }
}