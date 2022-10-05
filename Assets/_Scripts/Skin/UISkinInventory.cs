using UnityEngine;
using System.Collections.Generic;

public class UISkinInventory : MonoBehaviour
{
    public GameObject prefab;
    public Transform content;
    private List<UISkin> uiSkins = new List<UISkin>();

    public void ShowInventory(SkinType type)
    {
        this.ClearContent();

        foreach (Skin skin in GameManager.Instance.skins)
        {
            if (skin.profile.type == type)
            {
                this.CreateUISkin(skin);
            }
        }
    }

    public void ReloadTick()
    {
        foreach (UISkin uiSkin in uiSkins)
        {
            uiSkin.ReloadTick();
        }
    }

    private void ClearContent()
    {
        foreach (UISkin uiSkin in uiSkins)
        {
            uiSkin.SetSkin(null);
            Destroy(uiSkin.gameObject);
        }

        uiSkins.Clear();
    }

    private void CreateUISkin(Skin skin)
    {
        UISkin uiSkin = Instantiate(prefab, content).GetComponent<UISkin>();
        uiSkin.SetSkin(skin);

        uiSkins.Add(uiSkin);
    }
}