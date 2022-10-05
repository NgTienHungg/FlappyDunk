using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISkinButton : MonoBehaviour
{
    public Image fillImage;
    public Gradient gradient;

    public void OnEnable()
    {
        int total = GameManager.Instance.skins.Length;
        int owned = AchievementManager.Instance.TotalSkinOwned;

        float amount = (float)owned / total;
        fillImage.fillAmount = amount;
        fillImage.color = gradient.Evaluate(amount);
    }

    public void OnClick()
    {
        AudioManager.Instance.PlaySound("Pop");
        SceneManager.LoadScene("Skin");
    }
}