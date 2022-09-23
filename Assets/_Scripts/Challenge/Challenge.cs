using UnityEngine;

public class Challenge : MonoBehaviour
{
    public ChallengeProfile profile;

    [HideInInspector]
    public string key;

    [HideInInspector]
    public bool played = true;

    [HideInInspector]
    public bool passed = false;

    public void SetProfile(ChallengeProfile profile)
    {
        this.profile = profile;
        key = "Challenge" + profile.ID.ToString("00");

        played = PlayerPrefs.GetInt("Played" + key, 0) == 1 ? true : false;
        passed = PlayerPrefs.GetInt("Passed" + key, 0) == 1 ? true : false;
    }

    public void Play()
    {
        PlayerPrefs.SetInt("Played" + key, 1);
        played = true;
    }

    public void Pass()
    {
        PlayerPrefs.SetInt("Passed" + key, 1);
        passed = true;
    }
}