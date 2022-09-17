using UnityEngine;
using TMPro;

public class GPScoreText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        scoreText.text = GameController.Instance.Score.ToString();
    }

    private void Update()
    {
        scoreText.text = GameController.Instance.Score.ToString();
    }
}