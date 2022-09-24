using UnityEngine;
using UnityEngine.UI;

public enum SwitchKey
{
    OnSound,
    OnVibrate
}

public class UISwitchButton : MonoBehaviour
{
    public SwitchKey key;
    public Sprite onIcon, offIcon;
    private Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();

        if (PlayerPrefs.GetInt(key.ToString()) == 1)
            image.sprite = onIcon;
        else
            image.sprite = offIcon;
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
        image.sprite = onIcon;
    }

    private void TurnOff()
    {
        PlayerPrefs.SetInt(key.ToString(), 0);
        image.sprite = offIcon;
    }
}