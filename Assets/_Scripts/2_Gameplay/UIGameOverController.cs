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
        // prepare
        reviveButton.Disable();
        continueButton.interactable = false;
        tapToContinueText.color = Color.clear;

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
        continueButton.interactable = true;
        tapToContinueText.DOFade(1f, 0.5f);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}