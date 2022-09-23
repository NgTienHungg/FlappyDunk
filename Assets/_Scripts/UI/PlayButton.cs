using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Image ball, frontWing, backWing;

    private void Start()
    {
        ball.sprite = GameManager.Instance.GetSkin(SkinType.Ball, "BallSelecting").profile.ballSprite;
        frontWing.sprite = GameManager.Instance.GetSkin(SkinType.Wing, "WingSelecting").profile.wingSprite;
        backWing.sprite = GameManager.Instance.GetSkin(SkinType.Wing, "WingSelecting").profile.wingSprite;
    }

    public void OnClick()
    {
        GameController.Instance.OnPrepare();
    }
}