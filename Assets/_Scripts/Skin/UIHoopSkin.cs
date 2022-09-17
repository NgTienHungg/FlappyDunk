using UnityEngine;
using UnityEngine.UI;

public class UIHoopSkin : UISkin
{
    [SerializeField] private Image frontHoop, backHoop;

    public override void SetUp(SkinTab tab, SkinType type, int id)
    {
        base.SetUp(tab, type, id);
        frontHoop.sprite = GameManager.Instance.dataHoop.hoopSkins[id].frontHoop;
        backHoop.sprite = GameManager.Instance.dataHoop.hoopSkins[id].backHoop;
    }

    public void OnClick()
    {
        if (this.unlocked)
        {
            PlayerPrefs.SetInt("HoopSelecting", this.id);
            this.tab.UpdateTick();
        }
        else
            SkinTabManager.Instance.ShowInfoHoop(this.id);
    }
}