using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Image ball, frontWing, backWing;

    private void Update()
    {
        ball.sprite = GameManager.Instance.dataBall.ballSkins[PlayerPrefs.GetInt("BallSelecting")].sprite;
        frontWing.sprite = GameManager.Instance.dataWing.wingSkins[PlayerPrefs.GetInt("WingSelecting")].sprite;
        backWing.sprite = GameManager.Instance.dataWing.wingSkins[PlayerPrefs.GetInt("WingSelecting")].sprite;
    }

    public void OnClick()
    {
        GameController.Instance.OnPrepare();
    }
}