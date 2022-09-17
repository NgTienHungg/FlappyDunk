using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private SkinButton skinButton;
    [SerializeField] private ChallengeButton challengeButton;

    public void UpdateProgress()
    {
        skinButton.UpdateProgress();
        challengeButton.UpdateProgress();
    }
}