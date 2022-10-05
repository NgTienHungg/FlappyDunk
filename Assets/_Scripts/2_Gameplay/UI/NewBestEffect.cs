using UnityEngine;
using System.Collections;

public class NewBestEffect : MonoBehaviour
{
    public ParticleSystem[] stars;
    public float timeBetweenEffects;
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
        if (GameManager.Instance.gameMode == GameMode.Endless)
        {
            animator.SetTrigger("Trigger");
            StartCoroutine(PlayEffect());
        }
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