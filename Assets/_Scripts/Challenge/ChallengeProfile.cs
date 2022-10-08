using UnityEngine;

public enum ChallengeType
{
    PassAllHoop,
    StrongWing,
    FaceTheHoop
}

[CreateAssetMenu(menuName = "ScriptableObjects/Challenge")]
public class ChallengeProfile : ScriptableObject
{
    public int ID;
    public string description;
    public ChallengeType type;
    public GameObject level;

    #region ChallengeType = StrongWing
    public float flapForceY;
    #endregion
}