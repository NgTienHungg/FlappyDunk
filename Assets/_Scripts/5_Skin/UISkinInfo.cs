using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISkinInfo : MonoBehaviour
{
    public Image ball, wing, frontHoop, backHoop, flame;
    public TextMeshProUGUI description;

    private Skin skin;

    private void Awake()
    {
        this.OnDisable();
    }

    private void OnDisable()
    {
        ball.gameObject.SetActive(false);
        wing.gameObject.SetActive(false);
        frontHoop.gameObject.SetActive(false);
        backHoop.gameObject.SetActive(false);
        flame.gameObject.SetActive(false);
    }

    public void SetSkin(Skin skin)
    {
        this.skin = skin;

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

        description.text = skin.profile.description;
    }

    public void OnTrySkin()
    {
        // try skin
        GameManager.Instance.gameMode = GameMode.Trying;

        GameManager.Instance.skinTypeTrying = skin.profile.type;
        GameManager.Instance.skinTryingID = skin.profile.ID;

        SceneManager.LoadScene("Main");
    }
}