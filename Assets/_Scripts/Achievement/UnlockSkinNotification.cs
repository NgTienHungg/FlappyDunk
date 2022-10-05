using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnlockSkinNotification : MonoBehaviour
{
    public GameObject panel;
    public Image ball, wing, frontHoop, backHoop, flame;
    public TextMeshProUGUI description;

    private Queue<Skin> newSkins;

    private readonly float rotateDuration = 0.5f;
    private readonly Vector3 startRotation = new Vector3(0f, -90f, 0f);
    private readonly Vector3 endRotation = new Vector3(0f, 90f, 0f);

    private void OnEnable()
    {
        if (GameManager.Instance.gameMode != GameMode.Endless)
        {
            panel.SetActive(false);
            return;
        }

        newSkins = AchievementManager.Instance.newSkins;
        this.ShowNotify();
    }

    private void ClearBoard()
    {
        ball.gameObject.SetActive(false);
        wing.gameObject.SetActive(false);
        frontHoop.gameObject.SetActive(false);
        backHoop.gameObject.SetActive(false);
        flame.gameObject.SetActive(false);
    }

    public void ShowNotify()
    {
        if (newSkins.Count == 0)
        {
            panel.SetActive(false);
            return;
        }

        this.ClearBoard();

        Skin skins = newSkins.Dequeue();

        switch (skins.profile.type)
        {
            case SkinType.Ball:
                ball.gameObject.SetActive(true);
                ball.sprite = skins.profile.ballSprite;
                break;

            case SkinType.Wing:
                wing.gameObject.SetActive(true);
                wing.sprite = skins.profile.wingSprite;
                break;

            case SkinType.Hoop:
                frontHoop.gameObject.SetActive(true);
                backHoop.gameObject.SetActive(true);
                frontHoop.sprite = skins.profile.frontHoopSprite;
                backHoop.sprite = skins.profile.backHoopSprite;
                break;

            case SkinType.Flame:
                flame.gameObject.SetActive(true);
                flame.color = skins.profile.flameColor;
                break;
        }

        description.text = skins.profile.description;

        transform.localEulerAngles = startRotation;
        transform.DORotate(Vector3.zero, rotateDuration).SetEase(Ease.OutExpo).SetUpdate(true);
    }

    public void OnCancel()
    {
        AudioManager.Instance.PlaySound("Tap");
        transform.DORotate(endRotation, rotateDuration).SetEase(Ease.InExpo).SetUpdate(true)
            .OnComplete(ShowNotify);
    }

    public void OnShare()
    {
        AudioManager.Instance.PlaySound("Tap");
        // share something
    }
}