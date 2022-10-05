using UnityEngine;
using UnityEngine.SceneManagement;

public class UIChallengeManager : MonoBehaviour
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

    public void OnBackMenu()
    {
        AudioManager.Instance.PlaySound("Pop");
        SceneManager.LoadScene("Main");
    }
}