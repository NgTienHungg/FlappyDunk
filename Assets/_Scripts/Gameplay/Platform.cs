using DG.Tweening;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private SpriteRenderer core, outline;

    private void Awake()
    {
        this.core = transform.GetChild(0).GetComponent<SpriteRenderer>();
        this.outline = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    public void Appear(float appearDuration)
    {
        this.core.DOFade(1f, appearDuration).SetUpdate(true);
        this.outline.DOFade(1f, appearDuration).SetUpdate(true);
    }

    public void Fade(float fadeDuration)
    {
        this.core.DOFade(0f, fadeDuration).SetUpdate(true);
        this.outline.DOFade(0f, fadeDuration).SetUpdate(true);
    }
}