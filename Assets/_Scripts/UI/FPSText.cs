using UnityEngine;
using TMPro;

public class FPSText : MonoBehaviour
{
    private TextMeshProUGUI fpsText;
    private string prefix = "FPS: ";
    private float cooldownTimer;

    private void Awake()
    {
        this.fpsText = GetComponent<TextMeshProUGUI>();
        this.fpsText.text = prefix + ((int)(1f / Time.deltaTime)).ToString();
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            fpsText.text = prefix + "--";
            return;
        }

        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= 1f)
        {
            cooldownTimer = 0f;
            fpsText.text = prefix + ((int)(1f / Time.deltaTime)).ToString();
        }
    }
}