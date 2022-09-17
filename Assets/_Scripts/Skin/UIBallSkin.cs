using UnityEngine;
using UnityEngine.UI;

public class UIBallSkin : UISkin
{
    [SerializeField] protected Image preview;

    public override void SetUp(SkinTab tab, SkinType type, int id)
    {
        base.SetUp(tab, type, id);
        preview.sprite = GameManager.Instance.dataBall.ballSkins[id].sprite;
    }

    public void OnClick()
    {
        if (this.unlocked)
        {
            PlayerPrefs.SetInt("BallSelecting", this.id);
            this.tab.UpdateTick();
        }
        else
            SkinTabManager.Instance.ShowInfoBall(this.id);
    }
}