using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private void OnEnable()
    {
        MyEvent.BallFlaming += SwishXX;
    }

    private void OnDisable()
    {
        MyEvent.BallFlaming -= SwishXX;
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float timer = 0f;
        while (timer < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude * (duration - timer);
            float y = Random.Range(-1f, 1f) * magnitude * (duration - timer);
            transform.localPosition = new Vector3(x, y, -10f);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    private void SwishXX()
    {
        StartCoroutine(Shake(0.6f, 0.1f));
    }
}