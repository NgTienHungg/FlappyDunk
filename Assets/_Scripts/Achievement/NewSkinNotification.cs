using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewSkinNotification : MonoBehaviour
{
    public Image ball, wing, frontHoop, backHoop, flame;
    public TextMeshProUGUI condition;

    private Queue<string> newSkins = new Queue<string>();

    //private void OnEnable()
    //{

    //    Debug.Log("enabale skin");
    //    newSkins = ;

    //    this.ShowNotify();
    //}

    public void ShowNotify()
    {
        if (AchievementSystem.Instance.newSkins.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        Debug.Log("show skin");
        string code = AchievementSystem.Instance.newSkins.Dequeue();

        if (code.Contains("Ball"))
            this.NotifyNewBallSkin(int.Parse(code.Substring(4)));
        else if (code.Contains("Wing"))
            this.NotifyNewWingSkin(int.Parse(code.Substring(4)));
        else if (code.Contains("Hoop"))
            this.NotifyNewHoopSkin(int.Parse(code.Substring(4)));
        else if (code.Contains("Flame"))
            this.NotifyNewFlameSkin(int.Parse(code.Substring(5)));
    }

    private void NotifyNewBallSkin(int id)
    {
        ball.gameObject.SetActive(true);
        wing.gameObject.SetActive(false);
        frontHoop.gameObject.SetActive(false);
        backHoop.gameObject.SetActive(false);
        flame.gameObject.SetActive(false);

        BallSkin skin = GameManager.Instance.dataBall.ballSkins[id];
        ball.sprite = skin.sprite;
        condition.text = skin.condition;
    }

    private void NotifyNewWingSkin(int id)
    {
        wing.gameObject.SetActive(true);
        ball.gameObject.SetActive(false);
        frontHoop.gameObject.SetActive(false);
        backHoop.gameObject.SetActive(false);
        flame.gameObject.SetActive(false);

        WingSkin skin = GameManager.Instance.dataWing.wingSkins[id];
        wing.sprite = skin.sprite;
        condition.text = skin.condition;
    }

    private void NotifyNewHoopSkin(int id)
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
    }

    private void NotifyNewFlameSkin(int id)
    {
        flame.gameObject.SetActive(true);
        frontHoop.gameObject.SetActive(false);
        backHoop.gameObject.SetActive(false);
        wing.gameObject.SetActive(false);
        ball.gameObject.SetActive(false);

        FlameSkin skin = GameManager.Instance.dataFlame.flameSkins[id];
        flame.color = skin.color;
        condition.text = skin.condition;
    }

    public void OnOkButton()
    {
        this.ShowNotify();
    }
}