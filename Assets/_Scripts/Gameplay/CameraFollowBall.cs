using DG.Tweening;
using UnityEngine;

public class CameraFollowBall : MonoBehaviour
{
    [SerializeField] private Ball ball;
    private bool isFollowingBall;

    // setting
    private readonly float distanceWithBall = 1.5f;
    private readonly float timeMoveToBall = 0.5f;
    private readonly float timeMoveAfterDeath = 2f;

    private void Start() => this.FollowBall();

    public void FollowBall()
    {
        this.isFollowingBall = true;
        transform.DOKill(); // kill anim camera move after ball dead
        transform.DOMoveX(ball.transform.position.x + distanceWithBall, timeMoveToBall).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    private void FixedUpdate()
    {
        if (!this.isFollowingBall)
            return;

        if (!ball.IsAlive)
        {
            this.isFollowingBall = false;
            int direction = (int)Mathf.Sign(ball.GetComponent<Rigidbody2D>().velocity.x);
            transform.DOMoveX(transform.position.x + direction * 1f, timeMoveAfterDeath).SetEase(Ease.OutSine);
        }

        // move follow ball with fixedDeltaTime
        // use DOMove to have smooth movement
        transform.DOMoveX(ball.transform.position.x + distanceWithBall, Time.fixedDeltaTime);
    }
}