using UnityEngine;

public class Challenge : MonoBehaviour
{
    public ChallengeProfile profile;

    [HideInInspector]
    public bool isNew = true;

    [HideInInspector]
    public bool passed = false;

    private void Awake()
    {
        passed = PlayerPrefs.GetInt("PassedChallenge" + profile.ID) == 1 ? true : false;
    }
}