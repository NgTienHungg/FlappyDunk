using UnityEngine;
using TMPro;

public class UIChallenge : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public GameObject newIcon;

    private int idChallenge;

    public void SetUp(int id)
    {
        this.idChallenge = id;

        levelText.text = (this.idChallenge + 1).ToString();
        newIcon.SetActive(false);
    }

    public void OnClick()
    {
        //SceneManager.LoadScene("Level_" + idChallenge);
    }
}