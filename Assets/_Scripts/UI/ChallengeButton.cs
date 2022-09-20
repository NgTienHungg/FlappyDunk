using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChallengeButton : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient gradient;

    public void UpdateProgress()
    {
        int total = GameManager.Instance.challenges.Length;
        int passed = 0;

        foreach (Challenge challenge in GameManager.Instance.challenges)
            passed += challenge.passed ? 1 : 0;

        float progress = (float)passed / total;
        fillImage.fillAmount = progress;
        fillImage.color = gradient.Evaluate(progress);
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Challenge");
    }
}