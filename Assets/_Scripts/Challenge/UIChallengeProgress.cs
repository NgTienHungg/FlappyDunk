using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeProgress : MonoBehaviour
{
    public Image fillImage;
    public TextMeshProUGUI progressText;
    public Gradient gradient;

    private void Awake()
    {
        int total = GameManager.Instance.challenges.Length;
        int passed = 0;

        foreach (Challenge challenge in GameManager.Instance.challenges)
            passed += challenge.passed ? 1 : 0;

        float progress = (float)passed / total;
        fillImage.fillAmount = progress;
        fillImage.color = gradient.Evaluate(progress);

        progressText.text = string.Format($"{passed} / {total}");
    }
}