using DG.Tweening;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private Transform cameraTransform;
    private Transform left, mid, right; // idScene = -1, 0, 1
    private readonly float sceneDistance = 10.2f;
    private int mainScene;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        this.left = transform.GetChild(0);
        this.mid = transform.GetChild(1);
        this.right = transform.GetChild(2);
        mainScene = 0;
    }

    private void Update()
    {
        if (mainScene == 0)
        {
            if (cameraTransform.position.x >= this.right.transform.position.x)
            {
                this.left.transform.DOLocalMoveX(this.right.position.x + sceneDistance, 0f);
                mainScene = 1;
            }
        }
        else if (mainScene == 1)
        {
            if (cameraTransform.position.x >= this.left.transform.position.x)
            {
                this.mid.transform.DOLocalMoveX(this.left.position.x + sceneDistance, 0f);
                this.mainScene = -1;
            }
        }
        else if (mainScene == -1)
        {
            if (cameraTransform.position.x >= this.mid.transform.position.x)
            {
                this.right.transform.DOLocalMoveX(this.mid.position.x + sceneDistance, 0f);
                mainScene = 0;
            }
        }
    }
}