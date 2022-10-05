using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Entity")]
    public Ball ball;
    public Platform ceiling, floor;
    public HoopManager hoopManager;
    public SpriteRenderer finishLine;

    [Header("UI")]
    public UIMenuController uiMenu;
    public UIPlayController uiPlay;
    public UIPauseController uiPause;
    public UIGameOverController uiGameOver;
    public Image blackPanel;

    [HideInInspector]
    public bool IsPlaying, IsPrepare, HasSecondChance;

    // setting
    private readonly float prepareDuration = 0.8f;
    private readonly float reviveDuration = 0.3f;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        MyEvent.BallDead += OnGameOver;
        MyEvent.OnCompleteChallenge += CompleteChallenge;
    }

    private void OnDisable()
    {
        MyEvent.BallDead -= OnGameOver;
        MyEvent.OnCompleteChallenge -= CompleteChallenge;
    }

    public void Renew()
    {
        Time.timeScale = 0;

        ball.Fade(0f);
        floor.Fade(0f);
        ceiling.Fade(0f);

        this.HasSecondChance = true;
        this.IsPlaying = false;
        this.IsPrepare = false;
    }

    private void Start()
    {
        this.Renew();

        uiMenu.gameObject.SetActive(true);
        uiPlay.gameObject.SetActive(false);
        uiPause.gameObject.SetActive(false);
        uiGameOver.gameObject.SetActive(false);
        blackPanel.gameObject.SetActive(true);

        if (GameManager.Instance.gameMode == GameMode.Endless)
        {
            uiMenu.gameObject.SetActive(true);
            uiMenu.canvasGroup.interactable = true;
            blackPanel.gameObject.SetActive(false);
        }
        else
        {
            uiMenu.gameObject.SetActive(false);
            blackPanel.gameObject.SetActive(true);
            blackPanel.DOFade(0f, 0f).SetUpdate(true);

            this.OnPrepare();
        }
    }

    private void Update()
    {
        if (!this.IsPlaying)
            return;

        if (this.IsPrepare)
        {
            if (Input.GetMouseButtonDown(0) && !Util.IsPointerOverUIObject())
            {
                if (uiPause.gameObject.activeInHierarchy)
                    this.OnResume();
                else
                    this.OnPlay();
            }
        }
    }

    public void OnPrepare()
    {
        AudioManager.Instance.PlaySound("Whistle");

        if (GameManager.Instance.gameMode == GameMode.Endless)
            MyEvent.OnPlayEndlessMode?.Invoke();
        else if (GameManager.Instance.gameMode == GameMode.Trying)
            MyEvent.OnPlayTrySkinMode?.Invoke();
        else if (GameManager.Instance.gameMode == GameMode.Challenge)
            MyEvent.OnPlayChallengeMode?.Invoke();

        if (GameManager.Instance.gameMode == GameMode.Endless)
        {
            uiMenu.canvasGroup.interactable = false;
            uiMenu.transform.DOLocalMoveX(1200f, prepareDuration).SetEase(Ease.OutCubic).SetUpdate(true)
                .OnComplete(() =>
                {
                    uiMenu.transform.DOLocalMoveX(-1200f, 0f).SetUpdate(true)
                        .OnComplete(() =>
                        {
                            uiMenu.gameObject.SetActive(false);
                        });
                });
        }

        // prepare gameplay
        uiPlay.gameObject.SetActive(true);

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
        this.IsPrepare = false;
        Time.timeScale = 1;
    }

    public void OnPause()
    {
        uiPause.gameObject.SetActive(true);
        uiPlay.Disable();

        this.IsPrepare = true;
        Time.timeScale = 0;
    }

    public void OnResume()
    {
        uiPlay.gameObject.SetActive(true);
        uiPause.Disable();

        this.IsPrepare = false;
        Time.timeScale = 1;
    }

    private void OnGameOver()
    {
        uiPlay.Disable();
        uiGameOver.gameObject.SetActive(true);
    }

    public void OnSecondChance()
    {
        uiPlay.gameObject.SetActive(true);
        uiGameOver.Disable();

        MyEvent.OnUseSecondChance?.Invoke();

        StartCoroutine(OnRevive());
    }

    private IEnumerator OnRevive()
    {
        // clear gameplay
        ball.Fade(reviveDuration);
        this.HasSecondChance = false;
        this.IsPrepare = false;

        // wait for Ball fade complete
        yield return new WaitForSeconds(reviveDuration);

        ball.Revive();
        ball.IsAlive = false; // để bóng không nhận sk click chuột
        Time.timeScale = 0; // bóng không rơi tự do
        Camera.main.GetComponent<CameraFollowBall>().FollowBall();

        // wait for Camera move
        yield return new WaitForSecondsRealtime(reviveDuration);

        // reset gameplay
        ball.Appear(reviveDuration);

        // wait for Ball fade complete
        yield return new WaitForSecondsRealtime(reviveDuration);

        ball.IsAlive = true;
        IsPrepare = true;
    }

    public void OnBackHome()
    {
        uiGameOver.Disable();
        MyEvent.GameOver?.Invoke();

        // clear gameplay
        ball.Fade(prepareDuration);
        floor.Fade(prepareDuration);
        ceiling.Fade(prepareDuration);
        hoopManager.HoopFade(prepareDuration);

        if (GameManager.Instance.gameMode == GameMode.Challenge)
            StartCoroutine(HandlerAfterChallenge());
        else if (GameManager.Instance.gameMode == GameMode.Trying)
            StartCoroutine(HandleAfterTrySkin());
        else
            StartCoroutine(HandleAfterGameOver());
    }

    private IEnumerator HandleAfterGameOver()
    {
        // menu
        uiMenu.gameObject.SetActive(true);
        uiMenu.transform.DOLocalMoveX(0f, prepareDuration * 4 / 5).SetEase(Ease.OutBack);

        // wait gameover fade complete
        yield return new WaitForSeconds(prepareDuration);

        uiMenu.canvasGroup.interactable = true;

        this.Renew();
        ball.Revive();
        ball.transform.position = new Vector3(Camera.main.transform.position.x - 1.5f, 0f, 0f); // distanceWithCamera = 1.5f
        Camera.main.GetComponent<CameraFollowBall>().FollowBall();
        hoopManager.FreeHoops();
    }

    private IEnumerator HandleAfterTrySkin()
    {
        yield return new WaitForSeconds(prepareDuration / 4);

        blackPanel.DOFade(1f, prepareDuration * 3 / 4)
            .OnComplete(() =>
            {
                GameManager.Instance.gameMode = GameMode.Endless;
                SceneManager.LoadScene("Skin");
            });
    }

    private IEnumerator HandlerAfterChallenge()
    {
        finishLine.DOFade(0f, prepareDuration);

        yield return new WaitForSeconds(prepareDuration / 4);

        blackPanel.DOFade(1f, prepareDuration * 3 / 4)
            .OnComplete(() =>
            {
                GameManager.Instance.gameMode = GameMode.Endless;
                SceneManager.LoadScene("Challenge");
            });
    }

    public void CompleteChallenge()
    {
        Debug.Log("pass challenge");
        this.IsPlaying = false;
        uiPlay.Disable();
    }
}