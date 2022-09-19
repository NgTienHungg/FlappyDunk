using UnityEngine;
using UnityEngine.UI;

public enum SwitchKey
{
    OnSound,
    OnVibrate
}

public class SwitchButton : MonoBehaviour
{
    public SwitchKey key;
    public Sprite onIcon, offIcon;
    private Image image;

    private void Awake()
    {
        this.image = GetComponent<Image>();

        if (PlayerPrefs.GetInt(key.ToString()) == 1)
            this.image.sprite = onIcon;
        else
            this.image.sprite = offIcon;
    }

    public void OnClick()
    {
        if (PlayerPrefs.GetInt(key.ToString()) == 1)
            this.TurnOff();
        else
            this.TurnOn();
    }

    private void TurnOn()
    {
        PlayerPrefs.SetInt(key.ToString(), 1);
        this.image.sprite = onIcon;
    }

    private void TurnOff()
    {
        PlayerPrefs.SetInt(key.ToString(), 0);
        this.image.sprite = offIcon;
    }
}