using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private UISkinButton skinButton;
    [SerializeField] private UIChallengeButton challengeButton;

    public void UpdateProgress()
    {
        Debug.Log("update progress");
        skinButton.UpdateProgress();
        challengeButton.UpdateProgress();
    }
}