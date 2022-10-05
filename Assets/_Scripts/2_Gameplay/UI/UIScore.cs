using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        scoreText.text = ScoreManager.Instance.score.ToString();
    }

    private void Update()
    {
        scoreText.text = ScoreManager.Instance.score.ToString();
    }
}