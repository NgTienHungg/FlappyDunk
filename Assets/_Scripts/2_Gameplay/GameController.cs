using DG.Tweening;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Entity")]
    public Ball ball;
    public Platform ceiling, floor;
    public HoopManager hoopManager;
    public SpriteRenderer finishLine;
    public GameObject gz_challenge;

    [Header("UI")]
    public UIMenuController uiMenu;
    public UITutorial uiTutorial;
    public UIPlayController uiPlay;
    public UIPauseController uiPause;
    public UIGameOverController uiGameOver;

    public UISkinManager uiSkinPanel;
    public UIChallengeManager uiChallengePanel;

    [HideInInspector] public bool IsPlaying, IsPrepare, HasSecondChance;
    [HideInInspector] public GameObject level;

    // setting
    private readonly float prepareDuration = 0.8f;
    private readonly float reviveDuration = 0.3f;
    private readonly float changePanelDuration = 0.3f;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        MyEvent.BallDead += OnGameOver;
        MyEvent.OnCompleteChallenge += OnCompleteChallenge;
    }

    private void OnDisable()
    {
        MyEvent.BallDead -= OnGameOver;
        MyEvent.OnCompleteChallenge -= OnCompleteChallenge;
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

        ball.gameObject.SetActive(false);
        gz_challenge.gameObject.SetActive(false);
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

    /*---------- Endless ----------*/
    public void OnPlayEndless()
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

        this.OnPrepare();
        MyEvent.OnPlayEndlessMode?.Invoke();
    }

    public void OnPrepare()
    {
        AudioManager.Instance.PlaySound("Whistle");

        uiPlay.gameObject.SetActive(true);
        if (GameManager.Instance.gameMode != GameMode.Challenge && PlayerPrefs.GetInt("BestScore") == 0)
        {
            uiTutorial.gameObject.SetActive(true);
            uiPlay.gameObject.SetActive(false);
        }

        ball.gameObject.SetActive(true);
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
        // active uiPlay when has tutorial
        if (!uiPlay.gameObject.activeInHierarchy)
        {
            uiPlay.gameObject.SetActive(true);
            uiTutorial.gameObject.SetActive(false);
        }

        this.IsPrepare = false;
        Time.timeScale = 1;
    }

    public void OnPause()
    {
        AudioManager.Instance.PlaySound("Pop");

        uiPause.gameObject.SetActive(true);
        uiPlay.gameObject.SetActive(false);

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
        MyEvent.OnUseSecondChance?.Invoke();

        uiPlay.gameObject.SetActive(true);
        uiGameOver.Disable();

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
            StartCoroutine(FinishChallenge());
        else if (GameManager.Instance.gameMode == GameMode.Trying)
            StartCoroutine(FinishTrySkin());
        else
            StartCoroutine(FinishEndless());
    }

    private IEnumerator FinishEndless()
    {
        uiMenu.gameObject.SetActive(true);
        uiMenu.canvasGroup.interactable = false;
        uiMenu.transform.DOLocalMoveX(0f, prepareDuration * 4 / 5).SetEase(Ease.OutBack);

        // wait gameover fade complete
        yield return new WaitForSeconds(prepareDuration);
        uiMenu.canvasGroup.interactable = true;

        this.Renew();
        ball.Revive();
        ball.transform.position = new Vector3(Camera.main.transform.position.x - 1.5f, 0f);
        ball.gameObject.SetActive(false);
        Camera.main.GetComponent<CameraFollowBall>().FollowBall();
        hoopManager.FreeHoops();
    }

    private IEnumerator FinishTrySkin()
    {
        uiSkinPanel.gameObject.SetActive(true);
        uiSkinPanel.transform.DOLocalMoveX(0f, prepareDuration * 4 / 5).SetEase(Ease.OutBack).SetUpdate(true);

        yield return new WaitForSeconds(prepareDuration);
        uiSkinPanel.canvasGroup.interactable = true;
        GameManager.Instance.gameMode = GameMode.Endless;

        this.Renew();
        ball.Revive();
        ball.transform.position = new Vector3(Camera.main.transform.position.x - 1.5f, 0f);
        ball.gameObject.SetActive(false);
        Camera.main.GetComponent<CameraFollowBall>().FollowBall();
        hoopManager.FreeHoops();
    }

    private IEnumerator FinishChallenge()
    {
        finishLine.DOFade(0f, prepareDuration);

        uiChallengePanel.gameObject.SetActive(true);
        uiChallengePanel.transform.DOLocalMoveX(0f, prepareDuration * 4 / 5).SetEase(Ease.OutBack).SetUpdate(true);

        // wait gameover fade complete
        yield return new WaitForSeconds(prepareDuration);
        uiChallengePanel.canvasGroup.interactable = true;
        GameManager.Instance.gameMode = GameMode.Endless;

        Destroy(level);
        gz_challenge.gameObject.SetActive(false);

        this.Renew();
        ball.Revive();
        ball.transform.position = new Vector3(Camera.main.transform.position.x - 1.5f, 0f);
        ball.gameObject.SetActive(false);
        Camera.main.GetComponent<CameraFollowBall>().FollowBall();
        hoopManager.FreeHoops();
    }

    /*---------- Skin ----------*/
    public void OnOpenSkinPanel()
    {
        AudioManager.Instance.PlaySound("Pop");

        uiMenu.canvasGroup.interactable = false;
        uiMenu.transform.DOLocalMoveX(-1200f, changePanelDuration).SetEase(Ease.OutCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                uiMenu.gameObject.SetActive(false);
            });

        uiSkinPanel.gameObject.SetActive(true);
        uiSkinPanel.canvasGroup.interactable = false;
        uiSkinPanel.transform.DOLocalMoveX(0f, changePanelDuration).SetEase(Ease.OutCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                uiSkinPanel.canvasGroup.interactable = true;
            });
    }

    public void OnCloseSkinPanel()
    {
        AudioManager.Instance.PlaySound("Pop");

        uiMenu.gameObject.SetActive(true);
        uiMenu.transform.DOLocalMoveX(0f, changePanelDuration).SetEase(Ease.OutCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                uiMenu.canvasGroup.interactable = true;
            });

        uiSkinPanel.canvasGroup.interactable = false;
        uiSkinPanel.transform.DOLocalMoveX(1200f, changePanelDuration).SetEase(Ease.OutCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                uiSkinPanel.gameObject.SetActive(false);
            });
    }

    public void OnTrySkin()
    {
        uiSkinPanel.canvasGroup.interactable = false;
        uiSkinPanel.transform.DOLocalMoveX(-1200f, changePanelDuration).SetEase(Ease.OutCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                uiSkinPanel.gameObject.SetActive(false);
            });

        this.OnPrepare();
        MyEvent.OnPlayTrySkinMode?.Invoke();
    }

    /*---------- Challenge ----------*/
    public void OnOpenChallengePanel()
    {
        AudioManager.Instance.PlaySound("Pop");

        uiMenu.canvasGroup.interactable = false;
        uiMenu.transform.DOLocalMoveX(-1200f, changePanelDuration).SetEase(Ease.OutCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                uiMenu.gameObject.SetActive(false);
            });

        uiChallengePanel.gameObject.SetActive(true);
        uiChallengePanel.canvasGroup.interactable = false;
        uiChallengePanel.transform.DOLocalMoveX(0f, changePanelDuration).SetEase(Ease.OutCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                uiChallengePanel.canvasGroup.interactable = true;
            });
    }

    public void OnCloseChallengePanel()
    {
        AudioManager.Instance.PlaySound("Pop");

        uiMenu.gameObject.SetActive(true);
        uiMenu.transform.DOLocalMoveX(0f, changePanelDuration).SetEase(Ease.OutCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                uiMenu.canvasGroup.interactable = true;
            });

        uiChallengePanel.canvasGroup.interactable = false;
        uiChallengePanel.transform.DOLocalMoveX(1200f, changePanelDuration).SetEase(Ease.OutCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                uiChallengePanel.gameObject.SetActive(false);
            });
    }

    public void OnPlayChallenge()
    {
        uiChallengePanel.canvasGroup.interactable = false;
        uiChallengePanel.transform.DOLocalMoveX(-1200f, changePanelDuration).SetEase(Ease.OutCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                uiChallengePanel.gameObject.SetActive(false);
            });

        level = Instantiate(GameManager.Instance.ChallengePlaying.profile.level);
        gz_challenge.gameObject.SetActive(true);
        finishLine.DOFade(1f, 0f).SetUpdate(true);

        this.OnPrepare();
        MyEvent.OnPlayChallengeMode?.Invoke();
    }

    public void OnCompleteChallenge()
    {
        this.IsPlaying = false;
        uiPlay.Disable();
    }
}