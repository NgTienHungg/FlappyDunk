using UnityEngine;
using UnityEngine.UI;

public class UIWingSkin : UISkin
{
    [SerializeField] private Image preview;

    public override void SetUp(SkinTab tab, SkinType type, int id)
    {
        base.SetUp(tab, type, id);
        preview.sprite = GameManager.Instance.dataWing.wingSkins[id].sprite;
    }

    public void OnClick()
    {
        if (this.unlocked)
        {
            PlayerPrefs.SetInt("WingSelecting", this.id);
            this.tab.UpdateTick();
        }
        else
            SkinTabManager.Instance.ShowInfoWing(this.id);
    }
}