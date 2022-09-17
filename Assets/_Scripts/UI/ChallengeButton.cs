using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChallengeButton : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient gradient;

    public void UpdateProgress()
    {
        float amount = (float)GameManager.Instance.CountOfPassedChallenge / GameManager.Instance.TotalOfChallenge;
        fillImage.fillAmount = amount;
        fillImage.color = gradient.Evaluate(amount);
    }

    public void OnClick()
    {
        Logger.Log("On click challenge button");
    }
}