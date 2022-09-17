using UnityEngine;

[System.Serializable]
public class HoopSkin
{
    public int id;
    public Sprite frontHoop, backHoop, star;
    [TextArea] public string condition;
    public int target;
}

[CreateAssetMenu(fileName = "NewDataHoop", menuName = "Data/Hoop")]
public class DataHoop : ScriptableObject
{
    public HoopSkin[] hoopSkins;
}