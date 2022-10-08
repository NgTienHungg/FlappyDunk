using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject uiUnlockSkin;

    private void OnEnable()
    {
        uiUnlockSkin.SetActive(true);
    }
}