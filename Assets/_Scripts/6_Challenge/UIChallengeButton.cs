using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIChallengeButton : MonoBehaviour
{
    public Image fillImage;
    public Gradient gradient;

    public void OnEnable()
    {
        int total = GameManager.Instance.challenges.Length;
        int completed = AchievementManager.Instance.TotalChallengeCompleted;

        float amount = (float)completed / total;
        fillImage.fillAmount = amount;
        fillImage.color = gradient.Evaluate(amount);
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Challenge");
    }
}