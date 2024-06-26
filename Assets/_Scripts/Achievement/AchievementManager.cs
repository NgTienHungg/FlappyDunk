using UnityEngine;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    public int TotalSkinOwned { get; private set; }
    public int TotalChallengeCompleted { get; private set; }
    private int totalHoopPassed;
    private int totalPointScored;
    private int totalSwishAchieved;
    private int totalEndlessModePlayed;
    private int totalChallengePlayed;
    private int totalSkinTried;
    private int totalVideoWatched;
    private int totalSecondChanceUsed;
    private int hoopPassedInAnEndlessGame;
    private int swishAchievedInAnEndlessGame;
    private int pointScoredInAnEndlessGame;
    private int highestSwishInAnEndlessGame;

    public Queue<Skin> newSkins;
    private Skin[] skins;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        this.LoadPlayerPrefs();

        newSkins = new Queue<Skin>();
    }

    private void OnEnable()
    {
        MyEvent.OnPassHoop += this.OnPassHoop;
        MyEvent.OnGetScore += this.OnGetScore;
        MyEvent.OnGetSwish += this.OnGetSwish;
        MyEvent.OnPlayEndlessMode += this.OnPlayEndlessMode;
        MyEvent.OnPlayChallengeMode += this.OnPlayChallengeMode;
        MyEvent.OnPlayTrySkinMode += this.OnPlayTrySkinMode;
        MyEvent.OnWatchVideoAd += this.OnWatchVideoAd;
        MyEvent.OnUseSecondChance += this.OnUseSecondChance;
        MyEvent.OnCompleteChallenge += this.OnCompleteChallenge;
    }

    private void OnDisable()
    {
        MyEvent.OnPassHoop -= this.OnPassHoop;
        MyEvent.OnGetScore -= this.OnGetScore;
        MyEvent.OnGetSwish -= this.OnGetSwish;
        MyEvent.OnPlayEndlessMode -= this.OnPlayEndlessMode;
        MyEvent.OnPlayChallengeMode -= this.OnPlayChallengeMode;
        MyEvent.OnPlayTrySkinMode -= this.OnPlayTrySkinMode;
        MyEvent.OnWatchVideoAd -= this.OnWatchVideoAd;
        MyEvent.OnUseSecondChance -= this.OnUseSecondChance;
        MyEvent.OnCompleteChallenge -= this.OnCompleteChallenge;
    }

    private void LoadPlayerPrefs()
    {
        TotalSkinOwned = PlayerPrefs.GetInt("TotalSkinOwned", 4);
        TotalChallengeCompleted = PlayerPrefs.GetInt("TotalChallengeCompleted", 0);
        totalHoopPassed = PlayerPrefs.GetInt("TotalHoopPassed");
        totalPointScored = PlayerPrefs.GetInt("TotalPointScored");
        totalSwishAchieved = PlayerPrefs.GetInt("TotalSwishAchieved");
        totalEndlessModePlayed = PlayerPrefs.GetInt("TotalEndlessModePlayed");
        totalChallengePlayed = PlayerPrefs.GetInt("TotalChallengePlayed");
        totalSkinTried = PlayerPrefs.GetInt("TotalSkinTried");
        totalVideoWatched = PlayerPrefs.GetInt("TotalVideoWatched");
        totalSecondChanceUsed = PlayerPrefs.GetInt("TotalSecondChanceUsed");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("TotalSkinOwned", TotalSkinOwned);
        PlayerPrefs.SetInt("TotalChallengeCompleted", TotalChallengeCompleted);
        PlayerPrefs.SetInt("TotalHoopPassed", totalHoopPassed);
        PlayerPrefs.SetInt("TotalPointScored", totalPointScored);
        PlayerPrefs.SetInt("TotalSwishAchieved", totalSwishAchieved);
        PlayerPrefs.SetInt("TotalEndlessModePlayed", totalEndlessModePlayed);
        PlayerPrefs.SetInt("TotalChallengePlayed", totalChallengePlayed);
        PlayerPrefs.SetInt("TotalSkinTried", totalSkinTried);
        PlayerPrefs.SetInt("TotalVideoWatched", totalVideoWatched);
        PlayerPrefs.SetInt("TotalSecondChanceUsed", totalSecondChanceUsed);
    }

    private void Start()
    {
        skins = GameManager.Instance.skins;
    }

    private void OnPassHoop()
    {
        totalHoopPassed++;
        hoopPassedInAnEndlessGame++;
        this.UpdateAchievement();
    }

    private void OnGetSwish()
    {
        totalSwishAchieved++;
        swishAchievedInAnEndlessGame++;
        highestSwishInAnEndlessGame = Mathf.Max(highestSwishInAnEndlessGame, ScoreManager.Instance.combo);
        this.UpdateAchievement();
    }

    private void OnGetScore()
    {
        totalPointScored++;
        pointScoredInAnEndlessGame++;
        this.UpdateAchievement();
    }

    private void OnPlayEndlessMode()
    {
        totalEndlessModePlayed++;
        pointScoredInAnEndlessGame = 0;
        hoopPassedInAnEndlessGame = 0;
        swishAchievedInAnEndlessGame = 0;
        highestSwishInAnEndlessGame = 0;
        this.UpdateAchievement();
    }

    private void OnPlayTrySkinMode()
    {
        totalSkinTried++;
        this.UpdateAchievement();
    }

    private void OnPlayChallengeMode()
    {
        totalChallengePlayed++;
        this.UpdateAchievement();
    }

    private void OnCompleteChallenge()
    {
        TotalChallengeCompleted = 0;
        foreach (var challenge in GameManager.Instance.challenges)
        {
            TotalChallengeCompleted += challenge.passed ? 1 : 0;
        }

        this.UpdateAchievement();
    }

    private void OnWatchVideoAd()
    {
        totalVideoWatched++;
        this.UpdateAchievement();
    }

    private void OnUseSecondChance()
    {
        totalSecondChanceUsed++;
        this.UpdateAchievement();
    }

    private void OnUnlockSkin(Skin skin)
    {
        skin.Unlock();
        newSkins.Enqueue(skin); // to notify new skin

        TotalSkinOwned++;
        this.UpdateAchievement();
    }

    private void UpdateAchievement()
    {
        foreach (Skin skin in skins)
        {
            if (skin.unlocked)
                continue;

            if (skin.key == "Ball01" && hoopPassedInAnEndlessGame == 8
                || skin.key == "Ball02" && TotalChallengeCompleted == 1
                || skin.key == "Ball03" && TotalSkinOwned == 10
                || skin.key == "Ball04" && totalSecondChanceUsed == 12
                || skin.key == "Ball05" && pointScoredInAnEndlessGame >= 30
                || skin.key == "Ball06" && totalEndlessModePlayed == 15
                || skin.key == "Ball07" && highestSwishInAnEndlessGame == 5
                || skin.key == "Ball08" && totalVideoWatched == 20
                || skin.key == "Ball09" && swishAchievedInAnEndlessGame == 15

                || skin.key == "Wing01" && totalHoopPassed == 50
                || skin.key == "Wing02" && TotalChallengeCompleted == 2
                || skin.key == "Wing03" && totalPointScored == 200
                || skin.key == "Wing04" && TotalSkinOwned == 15
                || skin.key == "Wing05" && pointScoredInAnEndlessGame == 50
                || skin.key == "Wing06" && totalSkinTried == 10
                || skin.key == "Wing07" && swishAchievedInAnEndlessGame == 8
                || skin.key == "Wing08" && totalSecondChanceUsed == 5
                || skin.key == "Wing09" && totalSwishAchieved == 10

                || skin.key == "Hoop01" && totalSkinTried == 3
                || skin.key == "Hoop02" && totalVideoWatched == 10
                || skin.key == "Hoop03" && totalHoopPassed == 30
                || skin.key == "Hoop04" && pointScoredInAnEndlessGame >= 70
                || skin.key == "Hoop05" && TotalSkinOwned == 20
                || skin.key == "Hoop06" && totalSwishAchieved == 40
                || skin.key == "Hoop07" && highestSwishInAnEndlessGame == 7
                || skin.key == "Hoop08" && totalChallengePlayed == 5
                || skin.key == "Hoop09" && totalEndlessModePlayed == 8

                || skin.key == "Flame01" && totalVideoWatched == 30
                || skin.key == "Flame02" && totalSecondChanceUsed == 20
                || skin.key == "Flame03" && TotalSkinOwned == 30
                || skin.key == "Flame04" && totalHoopPassed == 30
                || skin.key == "Flame05" && swishAchievedInAnEndlessGame == 20
                || skin.key == "Flame06" && TotalChallengeCompleted == 3
                || skin.key == "Flame07" && totalHoopPassed == 3)
            {
                this.OnUnlockSkin(skin);
            }
        }

        this.Save();
    }
}