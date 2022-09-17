using UnityEngine;

public class UISkin : MonoBehaviour
{
    [SerializeField] protected GameObject tickIcon, newIcon, shadow;
    public SkinTab tab;
    private SkinType type;
    protected int id;
    protected bool unlocked;
    protected string key;

    public virtual void SetUp(SkinTab tab, SkinType type, int id)
    {
        this.id = id;
        this.tab = tab;
        this.type = type;

        switch (this.type)
        {
            case SkinType.Ball:
                key = "Ball";
                break;
            case SkinType.Wing:
                key = "Wing";
                break;
            case SkinType.Hoop:
                key = "Hoop";
                break;
            case SkinType.Flame:
                key = "Flame";
                break;
        }

        this.SetUpTick();
        this.SetUpShadow();
        newIcon.SetActive(false);
    }

    public void SetUpShadow()
    {
        this.unlocked = PlayerPrefs.GetInt(key + id) == 1 ? true : false;
        if (this.unlocked)
            shadow.SetActive(false);
        else
            shadow.SetActive(true);
    }

    public void SetUpTick()
    {
        if (PlayerPrefs.GetInt(key + "Selecting") == this.id)
            tickIcon.SetActive(true);
        else
            tickIcon.SetActive(false);
    }
}