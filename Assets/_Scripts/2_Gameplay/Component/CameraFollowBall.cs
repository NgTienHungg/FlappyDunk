using DG.Tweening;
using UnityEngine;

public class CameraFollowBall : MonoBehaviour
{
    private Ball ball;
    private bool isFollowing;

    private Transform cameraHolder;

    private readonly float distanceWithBall = 1.5f;
    private readonly float timeMoveToBall = 0.5f;
    private readonly float timeMoveAfterBallDead = 2f;

    private void Start()
    {
        cameraHolder = transform.parent;
        ball = FindObjectOfType<Ball>();
        this.FollowBall();
    }

    public void FollowBall()
    {
        this.isFollowing = true;
        cameraHolder.DOKill(); // kill anim camera move after ball dead
        cameraHolder.DOMoveX(ball.transform.position.x + distanceWithBall, timeMoveToBall).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    public void UnFollowBall()
    {
        this.isFollowing = false;
        int direction = (int)Mathf.Sign(ball.GetComponent<Rigidbody2D>().velocity.x);
        cameraHolder.DOMoveX(cameraHolder.position.x + direction * 1f, timeMoveAfterBallDead).SetEase(Ease.OutSine);
    }

    private void FixedUpdate()
    {
        if (!this.isFollowing)
            return;

        if (!ball.IsAlive)
            this.UnFollowBall();

        // move follow ball with fixedDeltaTime
        // use DOMove to have smooth movement
        cameraHolder.DOMoveX(ball.transform.position.x + distanceWithBall, Time.fixedDeltaTime);
    }
}