using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReviveButton : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float waitingTime = 5f;
    private bool isWaiting;
    private float cooldownTimer;

    public void Disable()
    {
        transform.DOKill();
        fillImage.DOKill();

        cooldownTimer = waitingTime;
        isWaiting = false;

        transform.localScale = Vector3.zero;
        fillImage.color = Color.green;
        fillImage.fillAmount = 1f;
    }

    public void Enable()
    {
        transform.DOScale(Vector3.one, 0.5f)
            .OnComplete(() =>
            {
                isWaiting = true;

                // anim button
                transform.DOScale(Vector3.one * 1.15f, 0.6f).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);

                // color gradient
                fillImage.DOColor(Color.yellow, waitingTime / 2f)
                    .OnComplete(() =>
                    {
                        fillImage.DOColor(Color.red, waitingTime / 2f);
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
            fillImage.fillAmount = cooldownTimer / waitingTime;
        }
    }

    public void OnClick()
    {
        GameController.Instance.OnSecondChance();
    }
}