using UnityEngine;

public class Challenge : MonoBehaviour
{
    public ChallengeProfile profile;

    [HideInInspector]
    public bool played = true;

    [HideInInspector]
    public bool passed = false;

    private void Awake()
    {
        played = PlayerPrefs.GetInt("PlayedChallenge" + profile.ID) == 1 ? true : false;
        passed = PlayerPrefs.GetInt("PassedChallenge" + profile.ID) == 1 ? true : false;
    }

    public void Play()
    {
        PlayerPrefs.SetInt("PlayedChallenge" + profile.ID, 1);
        played = true;
    }

    public void Pass()
    {
        PlayerPrefs.SetInt("PassedChallenge" + profile.ID, 1);
        passed = true;
    }
}