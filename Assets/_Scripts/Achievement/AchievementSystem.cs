using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    private int totalHoopPassed;
    private int totalSwishAchieved;
    private int totalChallengeCompleted;
    private int totalEndlessModePlayed;
    private int totalSkinOwned;
    private int totalVideoWatched;
    private int totalSecondChanceUsed;

    private int hoopPassedInAnEndlessMode;
    private int scoreInAnEndlessMode;
    private int highestSwishInAnEndlessMode;

    public Queue<string> newSkins = new Queue<string>();

    public static AchievementSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        Debug.Log("awake");

        this.LoadPlayerPrefs();
    }

    private void OnEnable()
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
        totalHoopPassed = PlayerPrefs.GetInt("TotalHoopPassed", 0);
        totalSwishAchieved = PlayerPrefs.GetInt("TotalSwishAchieved", 0);
        totalChallengeCompleted = PlayerPrefs.GetInt("TotalChallengeCompleted", 0);
        totalEndlessModePlayed = PlayerPrefs.GetInt("TotalEndlessModePlayed", 0);
        totalSkinOwned = PlayerPrefs.GetInt("TotalSkinOwned", 4);
        totalVideoWatched = PlayerPrefs.GetInt("TotalVideoWatched", 0);
        totalSecondChanceUsed = PlayerPrefs.GetInt("TotalSecondChanceUsed", 0);
    }

    private void OnPassHoop()
    {
        Debug.Log("Pass hoop");
        totalHoopPassed++;
        hoopPassedInAnEndlessMode++;

        if (PlayerPrefs.GetInt("Ball1") == 0 && hoopPassedInAnEndlessMode >= 3)
        {
            PlayerPrefs.SetInt("Ball1", 1);
            newSkins.Enqueue("Ball1");
        }
    }

    private void OnGetSwish()
    {
        Debug.Log("Swish");
        totalSwishAchieved++;

        //! not good
        highestSwishInAnEndlessMode = GameController.Instance.Combo;
    }

    private void OnAddScore()
    {
        Debug.Log("add score");
        scoreInAnEndlessMode++;
    }

    private void OnPlayEndlessMode()
    {
        Debug.Log("play endless mode");

        totalEndlessModePlayed++;

        hoopPassedInAnEndlessMode = 0;
        scoreInAnEndlessMode = 0;
        highestSwishInAnEndlessMode = 0;
    }

    private void OnCompleteChallenge()
    {
        totalChallengeCompleted = 0;
        foreach (Challenge challenge in GameManager.Instance.challenges)
        {
            totalChallengeCompleted += challenge.passed ? 1 : 0;
        }

        //if ()
    }

    private void OnWatchVideoAd()
    {
    }

    private void OnUnlockSkin()
    {
        totalSkinOwned++;

    }

    private void OnUseSecondChance()
    {
    }
}