using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private UISwish swish;
    [SerializeField] private TextMeshProUGUI uiScore;

    [HideInInspector] public int score, combo;
    [HideInInspector] public bool isPerfect;

    private void Awake()
    {
        Instance = this;
        this.Renew();
    }

    private void Renew()
    {
        score = 0;
        combo = 1;
        isPerfect = true;
    }

    private void OnEnable()
    {
        MyEvent.PassHoop += AddScore;
        MyEvent.GameOver += GameOver;
        MyEvent.BallDead += BallDead;
    }

    private void OnDisable()
    {
        MyEvent.PassHoop -= AddScore;
        MyEvent.GameOver -= GameOver;
        MyEvent.BallDead -= BallDead;
    }

    private void BallDead()
    {
        combo = 1;
    }

    private void GameOver()
    {
        if (GameManager.Instance.gameMode == GameMode.Endless)
        {
            PlayerPrefs.SetInt("LastScore", score);

            if (score > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", score);
                AudioManager.Instance.PlaySound("NewBest");
                MyEvent.HasNewBest?.Invoke();
            }
        }

        this.Renew();
    }

    public void AddScore()
    {
        this.InvokeAchievementEvent();
        this.HandleInGamePlay();
        this.SfxAndUpdateBallState();
    }

    private void InvokeAchievementEvent()
    {
        if (GameManager.Instance.gameMode == GameMode.Endless)
        {
            MyEvent.OnPassHoop?.Invoke();
            MyEvent.OnGetScore?.Invoke();

            if (isPerfect)
            {
                MyEvent.OnGetSwish?.Invoke();
            }
        }
    }

    private void HandleInGamePlay()
    {
        if (isPerfect)
        {
            combo++;
            swish.Play(combo);
        }
        else
        {
            // reset for next turn
            combo = 1;
            isPerfect = true;
        }

        score += combo;

        uiScore.rectTransform.DOScale(Vector3.one * (1f + Mathf.Min(5, combo) / 10f), 0f)
            .OnComplete(() =>
            {
                uiScore.rectTransform.DOScale(Vector3.one, 0.3f);
            });
    }

    private void SfxAndUpdateBallState()
    {
        if (combo == 1)
        {
            MyEvent.BallNormal?.Invoke();
        }
        else if (combo == 2)
        {
            AudioManager.Instance.PlaySound("SwishX2");
            AudioManager.Instance.PlayVibrate();
            MyEvent.BallFuming?.Invoke();
        }
        else if (combo == 3)
        {
            AudioManager.Instance.PlaySound("SwishX3");
            AudioManager.Instance.PlayVibrate();
            MyEvent.BallFlaming?.Invoke();
        }
        else if (combo >= 4)
        {
            AudioManager.Instance.PlaySound("SwishX4");
            AudioManager.Instance.PlayVibrate();
            MyEvent.BallFlaming?.Invoke();
        }
    }
}