using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private UISkinButton skinButton;
    [SerializeField] private UIChallengeButton challengeButton;

    public void UpdateProgress()
    {
        skinButton.UpdateProgress();
        challengeButton.UpdateProgress();
    }
}