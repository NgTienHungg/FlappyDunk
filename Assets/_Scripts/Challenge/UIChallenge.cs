using UnityEngine;
using TMPro;

public class UIChallenge : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public GameObject newImage;

    private int idChallenge;

    public void SetUp(int id)
    {
        this.idChallenge = id;

        levelText.text = (this.idChallenge + 1).ToString();
        newImage.SetActive(false);
    }
}