using UnityEngine;
using UnityEngine.UI;

public class UIPlayButton : MonoBehaviour
{
    public Image ball, frontWing, backWing;

    private void Start()
    {
        ball.sprite = GameManager.Instance.GetSkinSelecting(SkinType.Ball).profile.ballSprite;
        frontWing.sprite = GameManager.Instance.GetSkinSelecting(SkinType.Wing).profile.wingSprite;
        backWing.sprite = GameManager.Instance.GetSkinSelecting(SkinType.Wing).profile.wingSprite;
    }

    public void OnClick()
    {
        GameController.Instance.OnPrepare();
    }
}