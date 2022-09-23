using UnityEngine;
using System.Collections.Generic;

public class AchievementManager : Singleton<AchievementManager>
{
    public int TotalHoopPassed { get; private set; }
    public int TotalSwishAchieved { get; private set; }
    public int TotalChallengeCompleted { get; private set; }
    public int TotalEndlessModePlayed { get; private set; }
    public int TotalSkinOwned { get; private set; }
    public int TotalVideoWatched { get; private set; }
    public int TotalSecondChanceUsed { get; private set; }

    public int hoopPassedInAnEndlessMode;
    public int scoreInAnEndlessMode;
    public int highestSwishInAnEndlessMode;

    public Queue<string> newSkinCodes;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        this.RegisterListener();
        this.LoadPlayerPrefs();

        newSkinCodes = new Queue<string>();

        Debug.Log("awake achievement");
        Debug.Log(TotalSkinOwned);
    }

    private void RegisterListener()
    {
        MyEvent.OnPassHoop += this.OnPassHoop;
        MyEvent.OnAchieveSwish += this.OnGetSwish;
        MyEvent.OnAddScore += this.OnAddScore;
        MyEvent.OnPlayEndlessMode += this.OnPlayEndlessMode;
        MyEvent.OnCompleteChallenge += this.OnCompleteChallenge;
        MyEvent.OnWatchVideoAd += this.OnWatchVideoAd;
        MyEvent.OnUnlockSkin += this.OnUnlockSkin;
        MyEvent.OnUseSecondChance += this.OnUseSecondChance;
    }

    private void LoadPlayerPrefs()
    {
        TotalHoopPassed = PlayerPrefs.GetInt("TotalHoopPassed", 0);
        TotalSwishAchieved = PlayerPrefs.GetInt("TotalSwishAchieved", 0);
        TotalChallengeCompleted = PlayerPrefs.GetInt("TotalChallengeCompleted", 0);
        TotalEndlessModePlayed = PlayerPrefs.GetInt("TotalEndlessModePlayed", 0);
        TotalSkinOwned = PlayerPrefs.GetInt("TotalSkinOwned", 4);
        TotalVideoWatched = PlayerPrefs.GetInt("TotalVideoWatched", 0);
        TotalSecondChanceUsed = PlayerPrefs.GetInt("TotalSecondChanceUsed", 0);
    }

    private void OnPassHoop()
    {
        TotalHoopPassed++;
        hoopPassedInAnEndlessMode++;

        if (PlayerPrefs.GetInt("Ball01") == 0 && hoopPassedInAnEndlessMode >= 3)
        {
            PlayerPrefs.SetInt("Ball01", 1);
            newSkinCodes.Enqueue("Ball01");
        }
    }

    private void OnGetSwish()
    {
        TotalSwishAchieved++;

        //! not good
        highestSwishInAnEndlessMode = GameController.Instance.Combo;
    }

    private void OnAddScore()
    {
        scoreInAnEndlessMode++;
    }

    private void OnPlayEndlessMode()
    {
        TotalEndlessModePlayed++;

        hoopPassedInAnEndlessMode = 0;
        scoreInAnEndlessMode = 0;
        highestSwishInAnEndlessMode = 0;
    }

    private void OnCompleteChallenge()
    {
        TotalChallengeCompleted = 0;
        foreach (Challenge challenge in GameManager.Instance.challenges)
        {
            TotalChallengeCompleted += challenge.passed ? 1 : 0;
        }
    }

    private void OnWatchVideoAd()
    {
    }

    private void OnUnlockSkin()
    {
        TotalSkinOwned++;

    }

    private void OnUseSecondChance()
    {
    }
}