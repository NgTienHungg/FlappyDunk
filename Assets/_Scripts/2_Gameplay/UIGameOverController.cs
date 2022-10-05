using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIGameOverController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public UIReviveButton reviveButton;
    public Button continueButton;
    public TextMeshProUGUI tapToContinueText;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        // scale X icon
        transform.DOScale(1.5f, 0f)
            .OnComplete(() =>
            {
                transform.DOScale(1f, 0.5f);
            });

        // prepare
        reviveButton.Disable();
        continueButton.interactable = false;
        tapToContinueText.DOFade(0f, 0f);

        StartCoroutine(ShowSecondChance());
    }

    private IEnumerator ShowSecondChance()
    {
        // wait to show Ads Button and "tap to continue"
        yield return new WaitForSeconds(1f);

        // ball.TargetHoop = null in the last hoop of challenge
        if (!GameController.Instance.HasSecondChance || FindObjectOfType<Ball>().TargetHoop == null)
        {
            yield return new WaitForSeconds(0.6f);
            GameController.Instance.OnBackHome();
            yield break;
        }

        reviveButton.Enable();

        tapToContinueText.DOFade(1f, 0.5f)
            .OnComplete(() =>
            {
                continueButton.interactable = true;
            });
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}