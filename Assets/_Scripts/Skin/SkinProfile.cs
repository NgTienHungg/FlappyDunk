using UnityEngine;

public enum SkinType
{
    Ball,
    Wing,
    Hoop,
    Flame
}

[CreateAssetMenu(menuName = "ScriptableObjects/Skin")]
public class SkinProfile : ScriptableObject
{
    public int ID;
    [TextArea] public string description;
    public SkinType type;

    #region SkinType = Ball
    public Sprite ballSprite;
    #endregion

    #region SkinType = Wing
    public Sprite wingSprite;
    #endregion

    #region SkinType = Hoop
    public Sprite frontHoopSprite;
    public Sprite backHoopSprite;
    public Sprite starSprite;
    #endregion

    #region SkinType = Flame;
    public Color flameColor;
    #endregion
}