using System;
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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private SkinProfile[] skinProfiles;
    [SerializeField] private ChallengeProfile[] challengeProfiles;

    [HideInInspector] public Skin[] skins;
    [HideInInspector] public Challenge[] challenges;

    [HideInInspector] public GameMode gameMode;
    [HideInInspector] public SkinType skinTryingType;
    [HideInInspector] public int skinTryingID;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Application.targetFrameRate = 60;
        this.gameMode = GameMode.Endless;

        this.InitSkins();
        this.InitChallenges();

        Logger.Log("manager awake");
    }

    /// <summary>
    /// yêu cầu phải có 1 game object mẫu ở trong gameManager, dùng để Instantiate
    /// </summary>
    private void InitSkins()
    {
        skins = new Skin[skinProfiles.Length];
        Transform skinBox = transform.GetChild(0);
        GameObject go = skinBox.GetChild(0).gameObject;

        for (int i = 0; i < skins.Length; i++)
        {
            skins[i] = Instantiate(go, skinBox).GetComponent<Skin>();
            skins[i].SetProfile(skinProfiles[i]);
            skins[i].gameObject.name = skins[i].key;
        }

        Destroy(go);
    }

    private void InitChallenges()
    {
        challenges = new Challenge[challengeProfiles.Length];
        Transform challengeBox = transform.GetChild(1);
        GameObject go = challengeBox.GetChild(0).gameObject;

        for (int i = 0; i < challenges.Length; i++)
        {
            challenges[i] = Instantiate(go, challengeBox).GetComponent<Challenge>();
            challenges[i].SetProfile(challengeProfiles[i]);
            challenges[i].gameObject.name = challenges[i].key;
        }

        Destroy(go);
    }

    public Skin GetSkin(SkinType type, string playerPrefKey)
    {
        // PlayerPrefs.GetInt(playerPrefKey, 0) : nếu chưa có key (mới tải game) thì id skin mặc định là 0
        Skin skin = Array.Find(skins, skin => (skin.profile.type == type && skin.profile.ID == PlayerPrefs.GetInt(playerPrefKey, 0)));
        if (skin != null)
            return skin;

        Debug.LogError("Can't find skin with type " + type.ToString());
        return null;
    }

    public Skin GetSkinTrying()
    {
        return Array.Find(skins, skin => (skin.profile.type == skinTryingType && skin.profile.ID == skinTryingID));
    }
}