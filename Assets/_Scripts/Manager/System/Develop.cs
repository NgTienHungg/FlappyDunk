using UnityEngine;
using UnityEditor;

public class Develop
{
    [MenuItem("Develop/Add second chance")]
    public static void AddSecondChance()
    {
        GameController.Instance.HasSecondChance = true;
    }

    [MenuItem("Develop/Clear")]
    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Develop/Unlock/Ball")]
    public static void UnlockRandomBall()
    {
        int len = GameManager.Instance.dataBall.ballSkins.Length;
        for (int i = 0; i < len; i++)
        {
            if (PlayerPrefs.GetInt("Ball" + i) == 0)
            {
                PlayerPrefs.SetInt("Ball" + i, 1);
                return;
            }
        }
    }

    [MenuItem("Develop/Unlock/Wing")]
    public static void UnlockRandomWing()
    {
        int len = GameManager.Instance.dataWing.wingSkins.Length;
        for (int i = 0; i < len; i++)
        {
            if (PlayerPrefs.GetInt("Wing" + i) == 0)
            {
                PlayerPrefs.SetInt("Wing" + i, 1);
                return;
            }
        }
    }

    [MenuItem("Develop/Unlock/Hoop")]
    public static void UnlockRandomHoop()
    {
        int len = GameManager.Instance.dataHoop.hoopSkins.Length;
        for (int i = 0; i < len; i++)
        {
            if (PlayerPrefs.GetInt("Hoop" + i) == 0)
            {
                PlayerPrefs.SetInt("Hoop" + i, 1);
                return;
            }
        }
    }

    [MenuItem("Develop/Unlock/Flame")]
    public static void UnlockRandomFlame()
    {
        int len = GameManager.Instance.dataFlame.flameSkins.Length;
        for (int i = 0; i < len; i++)
        {
            if (PlayerPrefs.GetInt("Flame" + i) == 0)
            {
                PlayerPrefs.SetInt("Flame" + i, 1);
                return;
            }
        }
    }

    [MenuItem("Develop/Update skin")]
    public static void UpdateSkin()
    {
        GameManager.Instance.CalculateCountOfSkin();
    }
}