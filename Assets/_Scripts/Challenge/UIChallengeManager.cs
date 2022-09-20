using UnityEngine.SceneManagement;

public class UIChallengeManager : Singleton<UIChallengeManager>
{
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

    public void CancelInfoBoard()
    {
        uiChallengeInfo.gameObject.SetActive(false);
    }

    public void OnBackMenu()
    {
        SceneManager.LoadScene("Main");
    }
}