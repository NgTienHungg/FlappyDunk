using UnityEngine;

public class UIChallengeManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public UIChallengeInfo uiChallengeInfo;

    private void Start()
    {
        uiChallengeInfo.gameObject.SetActive(false);
    }

    public void ShowInfoChallenge(Challenge challenge)
    {
        uiChallengeInfo.gameObject.SetActive(true);
        uiChallengeInfo.SetChallenge(challenge);
    }
}