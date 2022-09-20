using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChallenge : MonoBehaviour
{
    public Image background;
    public Color greenColor;
    public TextMeshProUGUI challengeName;
    public GameObject newIcon;

    private Challenge challenge;

    public void SetChallenge(Challenge challenge)
    {
        this.challenge = challenge;
        challengeName.text = challenge.profile.ID.ToString();

        if (challenge.played)
            newIcon.SetActive(false);

        if (challenge.passed)
        {
            challengeName.color = Color.white;
            background.color = greenColor;
        }
        else
        {
            challengeName.color = greenColor;
            background.color = Color.white;
        }
    }

    public void OnClick()
    {
        UIChallengeManager.Instance.ShowInfoChallenge(challenge);
    }
}