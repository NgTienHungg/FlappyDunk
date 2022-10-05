using DG.Tweening;
using UnityEngine;
using System.Collections;

public class NewBestEffect : MonoBehaviour
{
    public ParticleSystem[] stars;
    public float timeBetweenEffects;
    public GameObject newBestPanel;

    private void OnEnable()
    {
        MyEvent.HasNewBest += NewBest;
    }

    private void OnDisable()
    {
        MyEvent.HasNewBest -= NewBest;
    }

    public void NewBest()
    {
        if (GameManager.Instance.gameMode == GameMode.Endless)
        {
            newBestPanel.SetActive(true);
            StartCoroutine(PlayEffect());

            // disable new best panel
            newBestPanel.transform.DOScale(1f, 2.5f).SetUpdate(true)
                .OnComplete(() =>
                {
                    newBestPanel.SetActive(false);
                });
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