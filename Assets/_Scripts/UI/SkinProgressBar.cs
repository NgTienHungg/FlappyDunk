using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinProgressBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient gradient;
    [SerializeField] private TextMeshProUGUI progressText;

    private void Awake()
    {
        float amount = (float)GameManager.Instance.countOwnedSkin / GameManager.Instance.totalSkin;
        fillImage.fillAmount = amount;
        fillImage.color = gradient.Evaluate(amount);
        progressText.text = GameManager.Instance.countOwnedSkin.ToString() + " / " + GameManager.Instance.totalSkin.ToString();
    }
}