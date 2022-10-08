using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private void OnEnable()
    {
        MyEvent.BallFlaming += Shake;
    }

    private void OnDisable()
    {
        MyEvent.BallFlaming -= Shake;
    }

    private IEnumerator SCShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float timer = 0f;
        while (timer < duration)
        {
            // tránh TH đang shake camera và Pause game
            while (Time.timeScale == 0)
                yield return null;

            float x = Random.Range(-1f, 1f) * magnitude * (duration - timer);
            float y = Random.Range(-1f, 1f) * magnitude * (duration - timer);
            transform.localPosition = new Vector3(x, y, -10f);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    private void Shake()
    {
        StartCoroutine(SCShake(0.6f, 0.1f));
    }
}