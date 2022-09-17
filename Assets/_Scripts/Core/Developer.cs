using UnityEngine;
using UnityEditor;

public class Developer
{
    [MenuItem("Developer/Add second chance")]
    public static void AddSecondChance()
    {
        GameController.Instance.HasSecondChance = true;
    }

    [MenuItem("Developer/Clear")]
    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Developer/Unlock/Ball")]
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

    [MenuItem("Developer/Unlock/Wing")]
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

    [MenuItem("Developer/Unlock/Hoop")]
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

    [MenuItem("Developer/Unlock/Flame")]
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

    [MenuItem("Developer/Update skin")]
    public static void UpdateSkin()
    {
        GameManager.Instance.CalculateCountOfSkin();
    }
}