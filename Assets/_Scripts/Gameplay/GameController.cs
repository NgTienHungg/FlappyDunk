using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [Header("Game play")]
    public Ball ball;
    public Swish swish;
    public Platform ceiling, floor;
    public HoopManager hoopManager;
    public TextMeshProUGUI scoreUI;

    [Header("Game over")]
    public WatchAdsButton watchAdsButton;
    public Button continueButton;
    public GameObject tapToContinueText;

    [Header("UI")]
    public UIMenuController uiMenuController;
    public Image menuPanel;
    public GameObject UIMenu;
    public GameObject UIPlay;
    public GameObject UIPause;
    public GameObject UIGameOver;


    // gameplay
    [HideInInspector] public bool IsPlaying, IsPrepare, IsGameOver, IsPerfect;
    [HideInInspector] public bool HasSecondChance, HasNewBest;
    [HideInInspector] public int Score, Combo;

    // setting
    private readonly float prepareDuration = 0.8f;
    private readonly float gameOverDuration = 1f;
    private readonly float reviveDuration = 0.3f;



    public void Renew()
    {
        UIMenu.SetActive(true);
        UIPlay.SetActive(false);
        UIPause.SetActive(false);
        UIGameOver.SetActive(false);

        // update fill for skin, challenge button
        uiMenuController.UpdateProgress();
        menuPanel.enabled = false; // can't touch to the button

        Time.timeScale = 0;
        ball.Fade(0f);
        floor.Fade(0f);
        ceiling.Fade(0f);

        this.HasSecondChance = true;
        this.IsPlaying = false;
        this.IsPrepare = false;
        this.IsGameOver = false;
        this.IsPerfect = true;
        this.Score = 0;
        this.Combo = 1;
    }

    private void Start()
    {
        this.Renew();
        this.HasNewBest = false;

        // try mode
        if (GameManager.Instance.IsTrying)
            this.OnPrepare();
    }

    private void Update()
    {
        if (!this.IsPlaying)
            return;

        if (this.IsPrepare)
            if (Input.GetMouseButtonDown(0) && !Util.IsPointerOverUIObject())
                this.OnPlay();

        if (!ball.IsAlive && !this.IsGameOver)
            this.OnGameOver();
    }

    public void AddScore()
    {
        if (this.IsPerfect)
        {
            this.Combo++;
            swish.Play(this.Combo);
            AudioManager.Instance.PlayVibrate();
        }
        else
        {
            this.Combo = 1;
            this.IsPerfect = true; // reset for next hoop
        }

        this.Score += this.Combo;
        scoreUI.rectTransform.DOScale(Vector3.one * (1f + Mathf.Min(5, this.Combo) / 10f), 0f)
            .OnComplete(() =>
            {
                scoreUI.rectTransform.DOScale(Vector3.one, 0.3f);
            });
        ball.UpdateState(this.Combo);
    }


    #region EVENT
    public void OnPrepare()
    {
        AudioManager.Instance.PlaySound("Whistle");

        UIPlay.SetActive(true);
        menuPanel.enabled = true; // can't tap on UI in menu when it move to right

        if (GameManager.Instance.IsTrying)
            UIMenu.transform.DOLocalMoveX(-1200f, 0f).SetUpdate(true);
        else
            UIMenu.transform.DOLocalMoveX(1200f, prepareDuration).SetEase(Ease.OutCubic).SetUpdate(true)
                .OnComplete(() => { UIMenu.transform.DOLocalMoveX(-1200f, 0f).SetUpdate(true); });

        // prepare gameplay
        ball.Appear(prepareDuration);
        floor.Appear(prepareDuration);
        ceiling.Appear(prepareDuration);
        hoopManager.SetUpHoops();
        hoopManager.HoopAppear(prepareDuration);

        this.IsPlaying = true;
        this.IsPrepare = true;
    }

    private void OnPlay()
    {
        UIPlay.SetActive(true);
        UIPause.SetActive(false);

        Time.timeScale = 1;
        this.IsPrepare = false;
    }

    public void OnPause()
    {
        UIPlay.SetActive(false);
        UIPause.SetActive(true);

        Time.timeScale = 0;
        this.IsPrepare = true;
    }

    private void OnGameOver()
    {
        UIPlay.SetActive(false);
        UIGameOver.SetActive(true);

        StartCoroutine(WaitToNotifyGameOver());
    }

    private IEnumerator WaitToNotifyGameOver()
    {
        this.IsGameOver = true;
        swish.Disable();
        watchAdsButton.Disable();
        continueButton.interactable = false;
        tapToContinueText.SetActive(false);

        // wait to show ads button and "tap to continue"
        yield return new WaitForSeconds(gameOverDuration);

        if (!this.HasSecondChance)
        {
            yield return new WaitForSeconds(gameOverDuration / 2);
            this.OnBackHome();
            yield break;
        }

        watchAdsButton.Enable();
        continueButton.interactable = true;
        tapToContinueText.SetActive(true);
    }

    public void OnSecondChance()
    {
        UIPlay.SetActive(true);
        UIGameOver.SetActive(false);

        StartCoroutine(OnRevive());
    }

    private IEnumerator OnRevive()
    {
        // clear gameplay
        ball.Fade(reviveDuration);
        this.HasSecondChance = false;

        // wait for Ball fade complete
        yield return new WaitForSeconds(reviveDuration);

        ball.Revive();
        Time.timeScale = 0; // Ball doesn't fall freely
        Camera.main.GetComponent<CameraFollowBall>().FollowBall();

        // wait for Camera move
        yield return new WaitForSecondsRealtime(reviveDuration);

        // reset gameplay
        ball.Appear(reviveDuration);
        IsPrepare = true;
        IsGameOver = false;
        IsPerfect = true;
        Combo = 1;
    }

    public void OnBackHome()
    {
        UIPlay.SetActive(false);
        UIGameOver.SetActive(false);

        if (GameManager.Instance.IsTrying)
            StartCoroutine(HandleAfterTrySkin());
        else
            StartCoroutine(HandleAfterGameOver());
    }

    private IEnumerator HandleAfterGameOver()
    {
        // save score
        PlayerPrefs.SetInt("LastScore", this.Score);
        if (this.Score > PlayerPrefs.GetInt("BestScore"))
        {
            this.HasNewBest = true;
            PlayerPrefs.SetInt("BestScore", this.Score);
        }

        // clear gameplay
        ball.Fade(prepareDuration);
        floor.Fade(prepareDuration);
        ceiling.Fade(prepareDuration);
        hoopManager.HoopFade(prepareDuration);

        // menu
        UIMenu.transform.DOLocalMoveX(0f, prepareDuration * 4 / 5).SetEase(Ease.OutBack);

        // wait gameover fade complete
        yield return new WaitForSeconds(prepareDuration);

        this.Renew();
        ball.Revive();
        ball.transform.position = new Vector3(Camera.main.transform.position.x - 1.5f, 0f, 0f); // distanceWithCamera = 1.5f
        Camera.main.GetComponent<CameraFollowBall>().FollowBall();
        hoopManager.FreeHoops();
    }

    private IEnumerator HandleAfterTrySkin()
    {
        // clear gameplay
        ball.Fade(prepareDuration);
        floor.Fade(prepareDuration);
        ceiling.Fade(prepareDuration);
        hoopManager.HoopFade(prepareDuration);

        yield return new WaitForSeconds(prepareDuration * 4 / 5);

        GameManager.Instance.IsTrying = false;
        SceneManager.LoadScene("Skin");
    }
    #endregion
}