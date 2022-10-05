using UnityEngine;

[System.Serializable]
public class Skin : MonoBehaviour
{
    public SkinProfile profile;

    [HideInInspector]
    public string key;

    [HideInInspector]
    public bool seen;

    [HideInInspector]
    public bool unlocked;

    public void SetProfile(SkinProfile profile)
    {
        this.profile = profile;
        key = profile.type.ToString() + profile.ID.ToString("00"); // ex: Ball01, Wing21

        seen = PlayerPrefs.GetInt("Seen" + key, 0) == 1 ? true : false;
        unlocked = PlayerPrefs.GetInt("Unlocked" + key, 0) == 1 ? true : false;
        
        // skin mặc định
        if (profile.ID == 0 && !unlocked)
        {
            this.See();
            this.Unlock();
        }
    }

    public void See()
    {
        PlayerPrefs.SetInt("Seen" + key, 1);
        seen = true;
    }

    public void Unlock()
    {
        PlayerPrefs.SetInt("Unlocked" + key, 1);
        unlocked = true;
    }
}