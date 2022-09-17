using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private Sprite onIcon, offIcon;
    
    [Description("OnSound, OnVibrate")]
    [SerializeField] private string key;

    private Image image;

    private void Awake()
    {
        this.image = GetComponent<Image>();
        if (PlayerPrefs.GetInt(key) == 1)
            this.image.sprite = onIcon;
        else
            this.image.sprite = offIcon;
    }

    public void OnClick()
    {
        if (PlayerPrefs.GetInt(key) == 1)
            this.TurnOff();
        else
            this.TurnOn();
    }

    private void TurnOn()
    {
        this.image.sprite = onIcon;
        PlayerPrefs.SetInt(key, 1);
    }

    private void TurnOff()
    {
        this.image.sprite = offIcon;
        PlayerPrefs.SetInt(key, 0);
    }
}