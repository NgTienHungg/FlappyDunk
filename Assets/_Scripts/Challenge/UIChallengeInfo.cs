using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIChallengeInfo : MonoBehaviour
{
    private Challenge challenge;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public void SetChallenge(Challenge challenge)
    {
        this.challenge = challenge;
        title.text = "CHALLENGE " + challenge.profile.ID;
        description.text = challenge.profile.description;
    }

    public void OnCancelBoard()
    {
        UIChallengeManager.Instance.CancelInfoBoard();
    }

    public void OnPlayChallenge()
    {
        GameManager.Instance.gameMode = GameMode.Challenge;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + challenge.profile.ID);
    }
}