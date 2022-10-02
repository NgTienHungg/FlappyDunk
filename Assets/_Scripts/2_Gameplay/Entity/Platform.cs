using DG.Tweening;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer core, outline;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        core = transform.GetChild(0).GetComponent<SpriteRenderer>();
        outline = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        MyEvent.BallFlaming += Shake;
    }

    private void OnDisable()
    {
        MyEvent.BallFlaming -= Shake;
    }

    private void Shake()
    {
        animator.SetTrigger("Shake");
    }

    public void Appear(float appearDuration)
    {
        core.DOFade(1f, appearDuration).SetUpdate(true);
        outline.DOFade(1f, appearDuration).SetUpdate(true);
    }

    public void Fade(float fadeDuration)
    {
        core.DOFade(0f, fadeDuration).SetUpdate(true);
        outline.DOFade(0f, fadeDuration).SetUpdate(true);
    }
}