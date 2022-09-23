using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkinProgressBar : MonoBehaviour
{
    public Image fillImage;
    public TextMeshProUGUI progressText;
    public Gradient gradient;

    private void Awake()
    {
        int total = GameManager.Instance.skins.Length;
        int owned = AchievementManager.Instance.TotalSkinOwned;

        float amount = (float)owned / total;
        fillImage.fillAmount = amount;
        fillImage.color = gradient.Evaluate(amount);

        progressText.text = string.Format($"{owned} / {total}");
    }
}