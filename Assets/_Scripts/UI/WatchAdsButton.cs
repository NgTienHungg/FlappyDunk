using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WatchAdsButton : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float waitingTime = 5f;
    private bool isWaiting;
    private float cooldownTimer;

    public void Disable()
    {
        // reset
        this.transform.DOKill();
        this.fillImage.DOKill();
        cooldownTimer = waitingTime;
        isWaiting = false;

        this.transform.localScale = Vector3.zero;
        this.fillImage.color = Color.green;
        this.fillImage.fillAmount = 1f;
    }

    public void Enable()
    {
        this.transform.DOScale(Vector3.one, 0.5f)
            .OnComplete(() =>
            {
                isWaiting = true;

                // anim button
                this.transform.DOScale(Vector3.one * 1.15f, 0.6f).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);

                // color gradient
                this.fillImage.DOColor(Color.yellow, waitingTime / 2f)
                    .OnComplete(() =>
                    {
                        this.fillImage.DOColor(Color.red, waitingTime / 2f);
                    });
            });
    }

    public void Update()
    {
        if (isWaiting)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                GameController.Instance.OnBackHome();
                return;
            }
            this.fillImage.fillAmount = cooldownTimer / waitingTime;
        }
    }

    public void OnClick()
    {
        GameController.Instance.OnSecondChance();
    }
}