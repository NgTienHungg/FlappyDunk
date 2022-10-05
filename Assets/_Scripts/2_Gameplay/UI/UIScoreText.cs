using TMPro;
using UnityEngine;
using System.ComponentModel;

public class UIScoreText : MonoBehaviour
{
    [Description("BestScore, LastScore")]
    public string key;

    private TextMeshProUGUI text;

    private void Awake()
    {
        this.text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        this.text.text = PlayerPrefs.GetInt(key).ToString();
    }
}