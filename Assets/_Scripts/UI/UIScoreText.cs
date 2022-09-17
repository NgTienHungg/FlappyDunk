using UnityEngine;
using System.ComponentModel;
using TMPro;

public class UIScoreText : MonoBehaviour
{
    [Description("BestScore, LastScore")]
    [SerializeField] private string key;

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