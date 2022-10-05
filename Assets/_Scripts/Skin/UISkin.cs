using UnityEngine;
using UnityEngine.UI;

public class UISkin : MonoBehaviour
{
    [Header("Preview")]
    public Image ball;
    public Image wing;
    public Image frontHoop, backHoop;
    public Image flame;

    [Header("UI")]
    public GameObject tickIcon;
    public GameObject newIcon;
    public GameObject shadow;

    private Skin skin;

    private void Awake()
    {
        ball.gameObject.SetActive(false);
        wing.gameObject.SetActive(false);
        frontHoop.gameObject.SetActive(false);
        backHoop.gameObject.SetActive(false);
        flame.gameObject.SetActive(false);

        tickIcon.SetActive(false);
        newIcon.SetActive(false);
        shadow.SetActive(true);
    }

    public void SetSkin(Skin skin)
    {
        this.skin = skin;

        if (skin == null)
            return;

        switch (skin.profile.type)
        {
            case SkinType.Ball:
                ball.gameObject.SetActive(true);
                ball.sprite = skin.profile.ballSprite;
                break;
            case SkinType.Wing:
                wing.gameObject.SetActive(true);
                wing.sprite = skin.profile.wingSprite;
                break;
            case SkinType.Hoop:
                frontHoop.gameObject.SetActive(true);
                backHoop.gameObject.SetActive(true);
                frontHoop.sprite = skin.profile.frontHoopSprite;
                backHoop.sprite = skin.profile.backHoopSprite;
                break;
            case SkinType.Flame:
                flame.gameObject.SetActive(true);
                flame.color = skin.profile.flameColor;
                break;
        }

        if (skin.unlocked)
        {
            shadow.SetActive(false);

            if (!skin.seen) newIcon.SetActive(true);

            // BallSelecting, WingSelecting, HoopSelecting, FlameSelecting
            if (skin.profile.ID == PlayerPrefs.GetInt(skin.profile.type.ToString() + "Selecting"))
                tickIcon.SetActive(true);
        }
    }

    private void SeeSkin()
    {
        if (!skin.seen)
        {
            skin.See();
            newIcon.SetActive(false);
        }
    }

    public void ReloadTick()
    {
        if (skin.profile.ID == PlayerPrefs.GetInt(skin.profile.type.ToString() + "Selecting"))
            tickIcon.SetActive(true);
        else
            tickIcon.SetActive(false);
    }

    public void OnClick()
    {
        if (skin.unlocked)
        {
            this.SeeSkin();
            FindObjectOfType<UISkinManager>().SelectSkin(skin);
        }
        else
            FindObjectOfType<UISkinManager>().ShowSkinInfo(skin);
    }
}