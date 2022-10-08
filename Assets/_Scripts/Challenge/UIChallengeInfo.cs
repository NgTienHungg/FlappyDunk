using TMPro;
using UnityEngine;

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
        GameManager.Instance.gameMode = GameMode.Challenge;
        GameManager.Instance.ChallengePlaying = challenge;
        challenge.Play();

        gameObject.SetActive(false);
        GameController.Instance.OnPlayChallenge();
    }
}