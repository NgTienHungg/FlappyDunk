using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIChallengeInfo : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    private Challenge challenge;

    public void SetChallenge(Challenge challenge)
    {
        this.challenge = challenge;
        title.text = "CHALLENGE " + challenge.profile.ID;
        description.text = challenge.profile.description;
    }

    public void OnCancelBoard()
    {
        AudioManager.Instance.PlaySound("Tap");
        gameObject.SetActive(false);
    }

    public void OnPlayChallenge()
    {
        AudioManager.Instance.PlaySound("Tap");
        PlayerPrefs.SetInt("ChallengePlaying", challenge.profile.ID);
        challenge.Play();

        GameManager.Instance.gameMode = GameMode.Challenge;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + challenge.profile.ID);
    }
}