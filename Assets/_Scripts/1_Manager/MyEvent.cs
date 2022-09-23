public static class MyEvent
{
    public delegate void AchievementEvent();
    public static AchievementEvent OnPassHoop;
    public static AchievementEvent OnGetScore;
    public static AchievementEvent OnGetSwish;
    public static AchievementEvent OnPlayEndlessMode;
    public static AchievementEvent OnPlayChallengeMode;
    public static AchievementEvent OnPlayTrySkinMode;
    public static AchievementEvent OnWatchVideoAd;
    public static AchievementEvent OnUseSecondChance;
    public static AchievementEvent OnCompleteChallenge;

    public delegate void UnlockSkin(Skin skin);
    public static UnlockSkin OnUnlockSkin;
    //public static AchievementEvent OnUnlockSkin;

    public delegate void GamePlayEvent();
    public static GamePlayEvent BallFuming;
    public static GamePlayEvent BallFlaming;
    public static GamePlayEvent BallDead;
}