using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISkinManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button ballButton;
    public Button wingButton;
    public Button hoopButton;
    public Button flameButton;
    public RectTransform separator;
    public Color activeColor;

    [Header("UI")]
    public GameObject infoPanel;
    public UISkinInfo uiSkinInfo;
    public UISkinInventory uiSkinInventory;

    private void Awake()
    {
        infoPanel.SetActive(false);
        this.OnBallTab();
        //separator.DOAnchorPosX(ballButton.transform.position.x, 1f);
    }

    public void OnBallTab()
    {
        this.ActiceButton(ballButton);
        this.DeactiveButton(wingButton);
        this.DeactiveButton(hoopButton);
        this.DeactiveButton(flameButton);

        uiSkinInventory.ShowInventory(SkinType.Ball);
    }

    public void OnWingTab()
    {
        this.DeactiveButton(ballButton);
        this.ActiceButton(wingButton);
        this.DeactiveButton(hoopButton);
        this.DeactiveButton(flameButton);

        uiSkinInventory.ShowInventory(SkinType.Wing);
    }

    public void OnHoopTab()
    {
        this.DeactiveButton(ballButton);
        this.DeactiveButton(wingButton);
        this.ActiceButton(hoopButton);
        this.DeactiveButton(flameButton);

        uiSkinInventory.ShowInventory(SkinType.Hoop);
    }

    public void OnFlameTab()
    {
        this.DeactiveButton(ballButton);
        this.DeactiveButton(hoopButton);
        this.DeactiveButton(wingButton);
        this.ActiceButton(flameButton);

        uiSkinInventory.ShowInventory(SkinType.Flame);
    }

    private void ActiceButton(Button button)
    {
        TextMeshProUGUI text = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.color = activeColor;
        text.fontSize = 65;
    }

    private void DeactiveButton(Button button)
    {
        TextMeshProUGUI text = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.color = Color.black;
        text.fontSize = 55;
    }

    public void ShowSkinInfo(Skin skin)
    {
        infoPanel.SetActive(true);
        uiSkinInfo.SetSkin(skin);
    }

    public void SelectSkin(Skin skin)
    {
        PlayerPrefs.SetInt(skin.profile.type.ToString() + "Selecting", skin.profile.ID); // BallSelecting = ??
        uiSkinInventory.ReloadTick();
    }

    public void OnCancelSkinInfo()
    {
        infoPanel.SetActive(false);
    }

    public void OnBackMenu()
    {
        SceneManager.LoadScene("Main");
    }
}