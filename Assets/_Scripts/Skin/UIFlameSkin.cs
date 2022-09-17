using UnityEngine;
using UnityEngine.UI;

public class UIFlameSkin : UISkin
{
    [SerializeField] private Image preview;

    public override void SetUp(SkinTab tab, SkinType type, int id)
    {
        base.SetUp(tab, type, id);
        preview.color = GameManager.Instance.dataFlame.flameSkins[id].color;
    }

    public void OnClick()
    {
        if (this.unlocked)
        {
            PlayerPrefs.SetInt("FlameSelecting", this.id);
            this.tab.UpdateTick();
        }
        else
            SkinTabManager.Instance.ShowInfoFlame(this.id);
    }
}