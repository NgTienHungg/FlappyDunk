using System.Collections;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        StartCoroutine(WaitToDestroy());
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        Debug.Log("Destroy ne");
    }

    private void Flap()
    {
        animator.Play("Flap", 0, 0);
        rigidBody.velocity = new Vector2(0f, 0f);
        rigidBody.AddForce(new Vector3(100f, 300f), ForceMode2D.Force);
    }

    private void Dead()
    {
        Debug.Log("Ball dead");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Platform"))
            //MyEvent.OnBallDead?.Invoke();
    }
}