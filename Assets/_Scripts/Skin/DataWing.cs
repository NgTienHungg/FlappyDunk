using UnityEngine;

[System.Serializable]
public class WingSkin
{
    public int id;
    public Sprite sprite;
    [TextArea] public string condition;
    public int target;
}

[CreateAssetMenu(fileName = "NewDataWing", menuName = "Data/Wing")]
public class DataWing : ScriptableObject
{
    public WingSkin[] wingSkins;
}