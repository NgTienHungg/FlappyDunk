using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Congratulation : MonoBehaviour
{
    public ParticleSystem[] stars;
    public float timeBetweenEffects = 0.25f;
    public TextMeshProUGUI notification;

    private void OnEnable()
    {
        MyEvent.OnCompleteChallenge += Trigger;
    }

    private void OnDisable()
    {
        MyEvent.OnCompleteChallenge -= Trigger;
    }

    private void Start()
    {
        notification.DOFade(0f, 0f).SetUpdate(true);
    }

    public void Trigger()
    {
        AudioManager.Instance.PlaySound("NewBest");

        notification.DOFade(1f, 0.6f).SetEase(Ease.OutQuart).SetUpdate(true);
        notification.rectTransform.DOAnchorPosY(600f, 0.6f).SetEase(Ease.OutQuart).SetUpdate(true);

        StartCoroutine(PlayEffect());
    } 

    private IEnumerator PlayEffect()
    {
        foreach (ParticleSystem star in stars)
        {
            yield return new WaitForSeconds(timeBetweenEffects);
            star.Play();
        }
    }
}