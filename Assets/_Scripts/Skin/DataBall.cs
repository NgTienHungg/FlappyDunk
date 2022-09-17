using UnityEngine;

[System.Serializable]
public class BallSkin
{
    public int id;
    public Sprite sprite;
    [TextArea] public string condition;
    public int target;
}

[CreateAssetMenu(fileName = "NewDataBall", menuName = "Data/Ball")]
public class DataBall : ScriptableObject
{
    public BallSkin[] ballSkins;
}