using TMPro;
using UnityEngine;

public enum SkinType
{
    Ball,
    Wing,
    Hoop,
    Flame
}

public class SkinTab : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject separator;
    [SerializeField] private GameObject tab;

    [Header("Content")]
    [SerializeField] private SkinType type;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform content;
    private UISkin[] uiSkins;

    // setting
    private readonly Color activeColor = new Color(0.12f, 0.6f, 1f);

    private void Awake()
    {
        switch (type)
        {
            case SkinType.Ball:
                uiSkins = new UIBallSkin[GameManager.Instance.dataBall.ballSkins.Length];
                break;
            case SkinType.Wing:
                uiSkins = new UIWingSkin[GameManager.Instance.dataWing.wingSkins.Length];
                break;
            case SkinType.Hoop:
                uiSkins = new UIHoopSkin[GameManager.Instance.dataHoop.hoopSkins.Length];
                break;
            case SkinType.Flame:
                uiSkins = new UIFlameSkin[GameManager.Instance.dataFlame.flameSkins.Length];
                break;
        }

        for (int i = 0; i < uiSkins.Length; i++)
        {
            uiSkins[i] = Instantiate(prefab, content).GetComponent<UISkin>();
            uiSkins[i].SetUp(this, type, i);
        }
    }

    public void Active()
    {
        buttonText.color = activeColor;
        buttonText.fontSize = 65f;
        separator.SetActive(true);
        tab.SetActive(true);
    }

    public void Deactive()
    {
        buttonText.color = Color.black;
        buttonText.fontSize = 55f;
        separator.SetActive(false);
        tab.SetActive(false);
    }

    /* call when on click a UI SKin */
    public void UpdateTick()
    {
        foreach (UISkin uiSkin in uiSkins)
            uiSkin.SetUpTick();
    }
}