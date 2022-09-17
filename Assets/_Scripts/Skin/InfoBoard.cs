using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoBoard : MonoBehaviour
{
    [SerializeField] private Image ball, wing, frontHoop, backHoop, flame;
    [SerializeField] private TextMeshProUGUI condition, progress;
    private string tryCode;
    private int tryID;

    public void ShowBallInfo(int id)
    {
        ball.gameObject.SetActive(true);
        wing.gameObject.SetActive(false);
        frontHoop.gameObject.SetActive(false);
        backHoop.gameObject.SetActive(false);
        flame.gameObject.SetActive(false);

        BallSkin skin = GameManager.Instance.dataBall.ballSkins[id];
        ball.sprite = skin.sprite;
        condition.text = skin.condition;

        tryCode = "Ball";
        tryID = id;
    }

    public void ShowWingInfo(int id)
    {
        wing.gameObject.SetActive(true);
        ball.gameObject.SetActive(false);
        frontHoop.gameObject.SetActive(false);
        backHoop.gameObject.SetActive(false);
        flame.gameObject.SetActive(false);

        WingSkin skin = GameManager.Instance.dataWing.wingSkins[id];
        wing.sprite = skin.sprite;
        condition.text = skin.condition;

        tryCode = "Wing";
        tryID = id;
    }

    public void ShowHoopInfo(int id)
    {
        frontHoop.gameObject.SetActive(true);
        backHoop.gameObject.SetActive(true);
        wing.gameObject.SetActive(false);
        ball.gameObject.SetActive(false);
        flame.gameObject.SetActive(false);

        HoopSkin skin = GameManager.Instance.dataHoop.hoopSkins[id];
        frontHoop.sprite = skin.frontHoop;
        backHoop.sprite = skin.backHoop;
        condition.text = skin.condition;

        tryCode = "Hoop";
        tryID = id;
    }

    public void ShowFlameInfo(int id)
    {
        flame.gameObject.SetActive(true);
        frontHoop.gameObject.SetActive(false);
        backHoop.gameObject.SetActive(false);
        wing.gameObject.SetActive(false);
        ball.gameObject.SetActive(false);

        FlameSkin skin = GameManager.Instance.dataFlame.flameSkins[id];
        flame.color = skin.color;
        condition.text = skin.condition;

        tryCode = "Flame";
        tryID = id;
    }

    public void OnTrySkin()
    {
        // save id skin selecting
        GameManager.Instance.IsTrying = true;
        GameManager.Instance.tryCode = tryCode;
        GameManager.Instance.tryID = tryID;

        SceneManager.LoadScene("Main");
    }
}