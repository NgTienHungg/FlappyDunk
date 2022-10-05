using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    public Image correctHoopArrow, wrongHoopArrow, wrongHoopCross, ceilingArrow, floorArrow;
    private float duration = 1f;

    private void OnEnable()
    {
        correctHoopArrow.rectTransform.anchoredPosition = new Vector3(200f, 180f);
        correctHoopArrow.transform.eulerAngles = new Vector3(0f, 0f, -30f);
        correctHoopArrow.DOFade(0f, 0f).SetUpdate(true);

        wrongHoopArrow.rectTransform.anchoredPosition = new Vector3(240f, -250f);
        wrongHoopArrow.transform.eulerAngles = new Vector3(0f, 0f, 30f);
        wrongHoopArrow.DOFade(0f, 0f).SetUpdate(true);

        wrongHoopCross.DOFade(0f, 0f).SetUpdate(true);

        ceilingArrow.rectTransform.anchoredPosition = new Vector3(380f, -320f);
        ceilingArrow.transform.eulerAngles = new Vector3(0f, 0f, 40f);

        floorArrow.rectTransform.anchoredPosition = new Vector3(-370f, 320f);
        floorArrow.transform.eulerAngles = new Vector3(0f, 0f, 40f);

        /*--------------------------------------------------*/

        correctHoopArrow.rectTransform.DOAnchorPos(new Vector3(260f, 120f), duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true);
        correctHoopArrow.rectTransform.DORotate(new Vector3(0f, 0f, -40f), duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true);
        correctHoopArrow.DOFade(1f, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true);

        wrongHoopArrow.rectTransform.DOAnchorPos(new Vector3(260f, -230f), duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true).SetDelay(duration);
        wrongHoopArrow.rectTransform.DORotate(new Vector3(0f, 0f, 40f), duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true).SetDelay(duration);
        wrongHoopArrow.DOFade(1f, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true).SetDelay(duration);

        wrongHoopCross.DOFade(1f, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true).SetDelay(duration);

        ceilingArrow.rectTransform.DOAnchorPos(new Vector3(400f, -300f), duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true);
        ceilingArrow.rectTransform.DORotate(new Vector3(0f, 0f, 50f), duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true);

        floorArrow.rectTransform.DOAnchorPos(new Vector3(-390f, 300f), duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true);
        floorArrow.rectTransform.DORotate(new Vector3(0f, 0f, 50f), duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetUpdate(true);
    }
}