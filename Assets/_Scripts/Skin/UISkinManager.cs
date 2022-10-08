using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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
    public CanvasGroup canvasGroup;
    public GameObject infoPanel;
    public UISkinInfo uiSkinInfo;
    public UISkinInventory uiSkinInventory;

    private readonly float separatorMovingTime = 0.4f;

    private void OnEnable()
    {
        infoPanel.SetActive(false);

        // active ball tab
        this.ActiceButton(ballButton);
        this.DeactiveButton(wingButton);
        this.DeactiveButton(hoopButton);
        this.DeactiveButton(flameButton);

        separator.DOMoveX(ballButton.transform.position.x, 0f).SetEase(Ease.OutBack).SetUpdate(true);

        uiSkinInventory.ShowInventory(SkinType.Ball);
    }

    public void OnBallTab()
    {
        AudioManager.Instance.PlaySound("Tap");

        this.ActiceButton(ballButton);
        this.DeactiveButton(wingButton);
        this.DeactiveButton(hoopButton);
        this.DeactiveButton(flameButton);

        separator.DOMoveX(ballButton.transform.position.x, separatorMovingTime).SetEase(Ease.OutBack).SetUpdate(true);

        uiSkinInventory.ShowInventory(SkinType.Ball);
    }

    public void OnWingTab()
    {
        AudioManager.Instance.PlaySound("Tap");

        this.DeactiveButton(ballButton);
        this.ActiceButton(wingButton);
        this.DeactiveButton(hoopButton);
        this.DeactiveButton(flameButton);

        separator.DOMoveX(wingButton.transform.position.x, separatorMovingTime).SetEase(Ease.OutBack).SetUpdate(true);

        uiSkinInventory.ShowInventory(SkinType.Wing);
    }

    public void OnHoopTab()
    {
        AudioManager.Instance.PlaySound("Tap");

        this.DeactiveButton(ballButton);
        this.DeactiveButton(wingButton);
        this.ActiceButton(hoopButton);
        this.DeactiveButton(flameButton);

        separator.DOMoveX(hoopButton.transform.position.x, separatorMovingTime).SetEase(Ease.OutBack).SetUpdate(true);

        uiSkinInventory.ShowInventory(SkinType.Hoop);
    }

    public void OnFlameTab()
    {
        AudioManager.Instance.PlaySound("Tap");

        this.DeactiveButton(ballButton);
        this.DeactiveButton(hoopButton);
        this.DeactiveButton(wingButton);
        this.ActiceButton(flameButton);

        separator.DOMoveX(flameButton.transform.position.x, separatorMovingTime).SetEase(Ease.OutBack).SetUpdate(true);

        uiSkinInventory.ShowInventory(SkinType.Flame);
    }

    private void ActiceButton(Button button)
    {
        // disable interacble to can't tap in this button
        button.interactable = false;

        TextMeshProUGUI text = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.color = activeColor;
        text.fontSize = 65;
    }

    private void DeactiveButton(Button button)
    {
        button.interactable = true;

        TextMeshProUGUI text = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.color = Color.black;
        text.fontSize = 60;
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
        AudioManager.Instance.PlaySound("Tap");
        infoPanel.SetActive(false);
    }
}