using UnityEngine;

public enum ChallengeType
{
    PassAllHoop,
    StrongSwing,
    FaceTheHoop
}

[CreateAssetMenu(menuName = "ScriptableObjects/Challenge")]
public class ChallengeProfile : ScriptableObject
{
    public int ID;
    public string description;

    public ChallengeType type;
    public float flapForceY;
}