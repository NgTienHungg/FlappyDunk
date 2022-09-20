using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient gradient;

    public void UpdateProgress()
    {
        float amount = (float)GameManager.Instance.countOwnedSkin / GameManager.Instance.totalSkin;
        fillImage.fillAmount = amount;
        fillImage.color = gradient.Evaluate(amount);
    }

    public void OnClick()
    {
        //ObjectPooler.Instance.FreePool("Hoop");
        SceneManager.LoadScene("Skin");
    }
}