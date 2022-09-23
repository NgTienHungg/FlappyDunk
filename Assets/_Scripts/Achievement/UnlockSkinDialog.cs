using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnlockSkinDialog : MonoBehaviour
{
    public Image ball, wing, frontHoop, backHoop, flame;
    public TextMeshProUGUI condition;

    private Queue<string> newSkinCodes;
    //private BallSkin ballSkin;
    //private WingSkin wingSkin;
    //private HoopSkin hoopSkin;
    //private FlameSkin flameSkin;

    private void OnEnable()
    {
        /// <summary>
        /// chỗ này mình không hiểu sao Awake và Enable của các Object bị gọi lẫn lộn, dẫn đến AchievementSystem.Instance vẫn đang null
        /// </summary>

        newSkinCodes = AchievementManager.Instance != null ? AchievementManager.Instance.newSkinCodes : new Queue<string>();

        this.ShowNotify();
    }

    private void OnDisable()
    {
        ball.gameObject.SetActive(false);
        wing.gameObject.SetActive(false);
        frontHoop.gameObject.SetActive(false);
        backHoop.gameObject.SetActive(false);
        flame.gameObject.SetActive(false);
    }

    public void ShowNotify()
    {
        if (newSkinCodes.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        string code = newSkinCodes.Dequeue();

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

        //ballSkin = GameManager.Instance.dataBall.ballSkins[id];
        //ball.sprite = ballSkin.sprite;
        //condition.text = ballSkin.condition;
    }

    private void NotifyNewWingSkin(int id)
    {
        wing.gameObject.SetActive(true);

        //wingSkin = GameManager.Instance.dataWing.wingSkins[id];
        //wing.sprite = wingSkin.sprite;
        //condition.text = wingSkin.condition;
    }

    private void NotifyNewHoopSkin(int id)
    {
        frontHoop.gameObject.SetActive(true);
        backHoop.gameObject.SetActive(true);

        //hoopSkin = GameManager.Instance.dataHoop.hoopSkins[id];
        //frontHoop.sprite = hoopSkin.frontHoop;
        //backHoop.sprite = hoopSkin.backHoop;
        //condition.text = hoopSkin.condition;
    }

    private void NotifyNewFlameSkin(int id)
    {
        flame.gameObject.SetActive(true);

        //flameSkin = GameManager.Instance.dataFlame.flameSkins[id];
        //flame.color = flameSkin.color;
        //condition.text = flameSkin.condition;
    }

    public void OnCancel()
    {
        this.ShowNotify();
    }

    public void OnShare()
    {
        // todo something
    }
}