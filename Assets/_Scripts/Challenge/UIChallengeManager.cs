using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIChallengeManager : MonoBehaviour
{
    public UIChallengeInfo uiChallengeInfo;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        uiChallengeInfo.gameObject.SetActive(false);

        canvasGroup.DOFade(0f, 0f).SetUpdate(true);
        canvasGroup.DOFade(1f, 0.5f).SetUpdate(true);
    }

    public void ShowInfoChallenge(Challenge challenge)
    {
        uiChallengeInfo.gameObject.SetActive(true);
        uiChallengeInfo.SetChallenge(challenge);
    }

    public void OnBackMenu()
    {
        SceneManager.LoadScene("Main");
    }
}