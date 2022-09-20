using UnityEngine;

/*
 * OnSound (int) : 0 = off, 1 = on
 * OnVirate
 * 
 * Ball + id (int) : 0 = lock, 1 = unlock
 * Wing + id
 * Hoop + id
 * Flame + id
 * 
 * BallSelecting (int)
 * WingSelecting
 * HoopSelecting
 * FlameSelecting
 */

public enum GameMode
{
    Endless,
    Trying,
    Challenge
}

public class GameManager : Singleton<GameManager>
{
    public DataBall dataBall;
    public DataWing dataWing;
    public DataHoop dataHoop;
    public DataFlame dataFlame;

    [HideInInspector] public int totalSkin, countOwnedSkin;
    [HideInInspector] public int totalChallenge = 3, countPassedChallenge;

    [HideInInspector] public GameMode gameMode;
    //[HideInInspector] public bool IsTrying;
    [HideInInspector] public string tryCode;
    [HideInInspector] public int tryID;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
        this.gameMode = GameMode.Endless;

        this.InitPlayerPrefs();
        this.CalculateCountOfSkin();
    }

    private void InitPlayerPrefs()
    {
        // SFX
        if (!PlayerPrefs.HasKey("OnSound"))
            PlayerPrefs.SetInt("OnSound", 1);

        if (!PlayerPrefs.HasKey("OnVibrate"))
            PlayerPrefs.SetInt("OnVibrate", 1);

        // skin
        if (!PlayerPrefs.HasKey("BallSelecting"))
        {
            PlayerPrefs.SetInt("Ball0", 1);
            PlayerPrefs.SetInt("BallSelecting", 0);
        }

        if (!PlayerPrefs.HasKey("WingSelecting"))
        {
            PlayerPrefs.SetInt("Wing0", 1);
            PlayerPrefs.SetInt("WingSelecting", 0);
        }

        if (!PlayerPrefs.HasKey("HoopSelecting"))
        {
            PlayerPrefs.SetInt("Hoop0", 1);
            PlayerPrefs.SetInt("HoopSelecting", 0);
        }

        if (!PlayerPrefs.HasKey("FlameSelecting"))
        {
            PlayerPrefs.SetInt("Flame0", 1);
            PlayerPrefs.SetInt("FlameSelecting", 0);
        }
    }

    public void CalculateCountOfSkin()
    {
        this.totalSkin = dataBall.ballSkins.Length + dataWing.wingSkins.Length + dataHoop.hoopSkins.Length + dataFlame.flameSkins.Length;

        this.countOwnedSkin = 0;
        foreach (var skin in dataBall.ballSkins)
            countOwnedSkin += PlayerPrefs.GetInt("Ball" + skin.id); // 0 or 1
        foreach (var skin in dataWing.wingSkins)
            countOwnedSkin += PlayerPrefs.GetInt("Wing" + skin.id);
        foreach (var skin in dataHoop.hoopSkins)
            countOwnedSkin += PlayerPrefs.GetInt("Hoop" + skin.id);
        foreach (var skin in dataFlame.flameSkins)
            countOwnedSkin += PlayerPrefs.GetInt("Flame" + skin.id);
    }
}