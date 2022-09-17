using UnityEngine;

[System.Serializable]
public class FlameSkin
{
    public int id;
    public Color color;
    [TextArea] public string condition;
    public int target;
}

[CreateAssetMenu(fileName = "NewDataFlame", menuName = "Data/Flame")]
public class DataFlame : ScriptableObject
{
    public FlameSkin[] flameSkins;
}