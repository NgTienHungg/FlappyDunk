using TMPro;
using UnityEngine;
using DG.Tweening;

public class UISwish : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string prefix = "SWISH\nX";

    private readonly Color colorSwishX2 = new Color(0f, 0.75f, 0f);
    private readonly Color colorSwishX3 = new Color(0.8f, 0.5f, 0f);
    private readonly Color colorSwishX4 = new Color(0.75f, 0f, 0.15f);

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        this.Disable();
    }

    public void Disable()
    {
        text.color = Color.clear;
        text.rectTransform.DOAnchorPosY(-500f, 0f);
    }

    public void Play(int combo)
    {
        // set up
        text.text = prefix + combo.ToString();
        switch (combo)
        {
            case 2:
                text.color = colorSwishX2;
                break;
            case 3:
                text.color = colorSwishX3;
                break;
            default:
                text.color = colorSwishX4;
                break;
        }

        // animation
        text.rectTransform.DOAnchorPosY(-400f, 1f).SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                text.DOFade(1f, 0.2f).OnComplete(() =>
                {
                    text.DOFade(0f, 0.5f).OnComplete(Disable);
                });
            });
    }
}