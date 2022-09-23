using UnityEngine;
using System.Collections.Generic;

public class HoopManager : MonoBehaviour
{
    [SerializeField] private Vector2 randomRangeVertical = new Vector2(-2f, 2f);
    [SerializeField] private List<HoopHolder> hoopHolders = new List<HoopHolder>();
    private float lastHoopPositionX;

    private readonly int numberOfHoops = 3;
    private readonly float distanceWithCamera = 1.5f;
    private readonly float distanceBetweenHoops = 3.8f;
    private readonly Vector3 lowInclination = new Vector3(0f, 0f, 15f);
    private readonly Vector3 normalInclination = new Vector3(0f, 0f, 25f);
    private readonly Vector3 highInclination = new Vector3(0f, 0f, 35f);

    /* call when prepare play game */
    public void SetUpHoops()
    {
        if (GameManager.Instance.gameMode != GameMode.Challenge)
        {
            lastHoopPositionX = Camera.main.transform.position.x + distanceWithCamera;
            for (int i = 0; i < numberOfHoops; i++)
                this.Append();
        }
        else
        {
            foreach (HoopHolder hoopHolder in hoopHolders)
                hoopHolder.LoadSkin();
        }

        // set first hoop in the middle
        hoopHolders[0].transform.position = new Vector3(hoopHolders[0].transform.position.x, 0f);
        hoopHolders[0].SetTarget();

        // hide all hoops
        this.HoopFade(0f);
    }

    private void FixedUpdate()
    {
        if (!GameController.Instance.IsPlaying || hoopHolders.Count == 0)
            return;

        if (!hoopHolders[0].IsTargeting)
        {
            hoopHolders.RemoveAt(0);

            // challenge
            if (hoopHolders.Count == 0)
            {
                GameController.Instance.ball.TargetHoop = null;
                GameController.Instance.ball.TargetHoopHolder = null;
                return;
            }

            hoopHolders[0].SetTarget();

            if (GameManager.Instance.gameMode != GameMode.Challenge)
                this.Append();
        }
    }

    private void Append()
    {
        HoopHolder hoopHolder = PoolManager.Instance.Spawn(ObjectTag.Hoop).GetComponent<HoopHolder>();
        hoopHolder.transform.position = new Vector3(lastHoopPositionX, Random.Range(randomRangeVertical.x, randomRangeVertical.y));
        hoopHolder.LoadSkin();

        int score = GameController.Instance.Score;
        if (score >= 15)
        {
            int rate = Random.Range(0, 100);
            if (score >= 15 && rate <= 40)
                hoopHolder.transform.eulerAngles = lowInclination;
            if (score >= 100 && rate <= 30)
                hoopHolder.transform.eulerAngles = normalInclination;
            if (score >= 300 && rate <= 20)
                hoopHolder.transform.eulerAngles = highInclination;

            // flip hoop
            if (score >= 50)
                hoopHolder.transform.eulerAngles *= Random.Range(0, 2) == 1 ? 1 : -1;

            // from 30 score && 50% can move
            if (score >= 30 && rate <= 50)
                hoopHolder.SetCanMove();
        }

        hoopHolders.Add(hoopHolder);
        lastHoopPositionX += distanceBetweenHoops;
    }

    public void HoopAppear(float appearDuration)
    {
        foreach (HoopHolder hoopHolder in hoopHolders)
            hoopHolder.Appear(appearDuration);
    }

    public void HoopFade(float fadeDuration)
    {
        foreach (HoopHolder hoopHolder in hoopHolders)
            hoopHolder.Fade(fadeDuration);
    }

    /* call when gameover and go back to menu */
    public void FreeHoops()
    {
        foreach (HoopHolder hoopHolder in hoopHolders)
        {
            hoopHolder.Renew();
            hoopHolder.gameObject.SetActive(false);
        }
        hoopHolders.Clear();
    }
}