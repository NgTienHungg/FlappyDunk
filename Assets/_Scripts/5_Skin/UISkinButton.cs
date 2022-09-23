using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISkinButton : MonoBehaviour
{
    public Image fillImage;
    public Gradient gradient;

    public void UpdateProgress()
    {
        int total = GameManager.Instance.skins.Length;
        int owned = AchievementManager.Instance.TotalSkinOwned;

        float amount = (float)owned / total;
        fillImage.fillAmount = amount;
        fillImage.color = gradient.Evaluate(amount);
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Skin");
    }
}