using UnityEngine;
using TMPro;

public class UIChallenge : MonoBehaviour
{
    private Challenge challenge;
    public TextMeshProUGUI challengeName;
    public GameObject newIcon;

    public void SetChallenge(Challenge challenge)
    {
        this.challenge = challenge;
        this.challengeName.text = challenge.profile.ID.ToString();
        this.newIcon.SetActive(true);
    }

    public void OnClick()
    {
        UIChallengeManager.Instance.ShowInfoChallenge(this.challenge);
    }
}