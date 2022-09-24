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
    
    public void Trigger()
    {
        AudioManager.Instance.PlaySound("NewBest");

        notification.gameObject.SetActive(true);
        notification.DOFade(0f, 0f);

        notification.DOFade(1f, 0.6f).SetEase(Ease.OutQuart).SetDelay(0.3f);
        notification.rectTransform.DOAnchorPosY(600f, 0.6f).SetEase(Ease.OutQuart).SetDelay(0.3f);

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