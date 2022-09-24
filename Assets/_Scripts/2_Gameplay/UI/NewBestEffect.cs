using UnityEngine;
using System.Collections;

public class NewBestEffect : MonoBehaviour
{
    public ParticleSystem[] stars;
    public float timeBetweenEffects = 0.25f;
    public Animator animator;

    private void OnEnable()
    {
        MyEvent.HasNewBest += Trigger;
    }

    private void OnDisable()
    {
        MyEvent.HasNewBest -= Trigger;
    }

    public void Trigger()
    {
        animator.SetTrigger("Trigger");
        StartCoroutine(PlayEffect());
    }

    private IEnumerator PlayEffect()
    {
        foreach (ParticleSystem star in stars)
        {
            yield return new WaitForSecondsRealtime(timeBetweenEffects);
            star.Play();
        }
    }
}