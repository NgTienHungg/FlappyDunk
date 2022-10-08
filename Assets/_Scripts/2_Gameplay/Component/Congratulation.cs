using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Congratulation : MonoBehaviour
{
    public ParticleSystem[] stars;
    public float timeBetweenEffects = 0.25f;
    public GameObject challengePanel;
    public TextMeshProUGUI message;

    private void OnEnable()
    {
        MyEvent.OnCompleteChallenge += Congratulate;
    }

    private void OnDisable()
    {
        MyEvent.OnCompleteChallenge -= Congratulate;
    }

    public void Congratulate()
    {
        AudioManager.Instance.PlaySound("NewBest");
        challengePanel.SetActive(true);

        message.rectTransform.DOAnchorPosY(400f, 0f).SetUpdate(true);
        message.rectTransform.DOAnchorPosY(700f, 1f).SetEase(Ease.OutQuart).SetUpdate(true);

        message.DOFade(0f, 0f).SetUpdate(true);
        message.DOFade(1f, 0.6f).SetEase(Ease.OutQuart).SetUpdate(true);

        StartCoroutine(PlayEffect());
    } 

    private IEnumerator PlayEffect()
    {
        foreach (ParticleSystem star in stars)
        {
            yield return new WaitForSeconds(timeBetweenEffects);
            star.Play();
        }

        challengePanel.SetActive(false);
    }
}