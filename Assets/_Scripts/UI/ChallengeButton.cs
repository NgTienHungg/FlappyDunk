using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChallengeButton : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient gradient;

    public void UpdateProgress()
    {
        float amount = (float)GameManager.Instance.countPassedChallenge / GameManager.Instance.totalChallenge;
        fillImage.fillAmount = amount;
        fillImage.color = gradient.Evaluate(amount);
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Challenge");
    }
}