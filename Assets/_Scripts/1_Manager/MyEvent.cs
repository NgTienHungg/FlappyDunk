public static class MyEvent
{
    public delegate void AchievementEvent();
    public static AchievementEvent OnPassHoop;
    public static AchievementEvent OnAchieveSwish;
    public static AchievementEvent OnAddScore;
    public static AchievementEvent OnPlayEndlessMode;
    public static AchievementEvent OnCompleteChallenge;
    public static AchievementEvent OnWatchVideoAd;
    public static AchievementEvent OnUnlockSkin;
    public static AchievementEvent OnUseSecondChance;

    public delegate void GamePlayEvent();
    public static GamePlayEvent BallFuming;
    public static GamePlayEvent BallFlaming;
    public static GamePlayEvent BallDead;
}