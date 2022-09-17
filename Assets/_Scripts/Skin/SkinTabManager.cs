using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinTabManager : Singleton<SkinTabManager>
{
    [Header("Tabs")]
    [SerializeField] private SkinTab ballTab;
    [SerializeField] private SkinTab wingTab;
    [SerializeField] private SkinTab hoopTab;
    [SerializeField] private SkinTab flameTab;

    [Header("Info board")]
    [SerializeField] private InfoBoard infoBoard;

    private void Start()
    {
        this.OnBallTab();
        infoBoard.gameObject.SetActive(false);
    }

    public void OnBallTab()
    {
        ballTab.Active();
        wingTab.Deactive();
        hoopTab.Deactive();
        flameTab.Deactive();
    }

    public void OnWingTab()
    {
        ballTab.Deactive();
        wingTab.Active();
        hoopTab.Deactive();
        flameTab.Deactive();
    }

    public void OnHoopTab()
    {
        ballTab.Deactive();
        wingTab.Deactive();
        hoopTab.Active();
        flameTab.Deactive();
    }

    public void OnFlameTab()
    {
        ballTab.Deactive();
        wingTab.Deactive();
        hoopTab.Deactive();
        flameTab.Active();
    }

    public void ShowInfoBall(int id)
    {
        infoBoard.gameObject.SetActive(true);
        infoBoard.ShowBallInfo(id);
    }

    public void ShowInfoWing(int id)
    {
        infoBoard.gameObject.SetActive(true);
        infoBoard.ShowWingInfo(id);
    }

    public void ShowInfoHoop(int id)
    {
        infoBoard.gameObject.SetActive(true);
        infoBoard.ShowHoopInfo(id);
    }

    public void ShowInfoFlame(int id)
    {
        infoBoard.gameObject.SetActive(true);
        infoBoard.ShowFlameInfo(id);
    }

    public void OnCloseInfoBoard()
    {
        infoBoard.gameObject.SetActive(false);
    }

    public void OnHome ()
    {
        SceneManager.LoadScene("Main");
    }
}