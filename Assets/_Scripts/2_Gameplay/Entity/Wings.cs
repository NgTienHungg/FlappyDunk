using UnityEngine;

public class Wings : MonoBehaviour
{
    [SerializeField] private Rigidbody2D frontWing, backWing;
    private Vector3 frontWingStartPos, backWingStartPos;

    private void Awake()
    {
        frontWing.bodyType = RigidbodyType2D.Kinematic;
        backWing.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Start()
    {
        frontWingStartPos = frontWing.transform.localPosition;
        backWingStartPos = backWing.transform.localPosition;
    }

    public void Explode()
    {
        frontWing.bodyType = RigidbodyType2D.Dynamic;
        backWing.bodyType = RigidbodyType2D.Dynamic;

        frontWing.AddForce(new Vector2(Random.Range(-50f, -20f), Random.Range(300f, 400f)));
        backWing.AddForce(new Vector2(Random.Range(100f, 120f), Random.Range(300f, 400f)));
    }

    public void Reset()
    {
        frontWing.transform.localPosition = frontWingStartPos;
        backWing.transform.localPosition = backWingStartPos;

        frontWing.bodyType = RigidbodyType2D.Kinematic;
        backWing.bodyType = RigidbodyType2D.Kinematic;

        frontWing.velocity = Vector2.zero;
        backWing.velocity = Vector2.zero;

        frontWing.angularVelocity = 0f;
        backWing.angularVelocity = 0f;
    }
}