using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeProgressBar : MonoBehaviour
{
    public Image fillImage;
    public TextMeshProUGUI progressText;
    public Gradient gradient;

    private void OnEnable()
    {
        int total = GameManager.Instance.challenges.Length;
        int completed = AchievementManager.Instance.TotalChallengeCompleted;

        float amount = (float)completed / total;
        fillImage.fillAmount = amount;
        fillImage.color = gradient.Evaluate(amount);

        progressText.text = string.Format($"{completed} / {total}");
    }
}