using UnityEngine;

public class UIChallengeManager : MonoBehaviour
{
    public GameObject uiChallengePrefab;
    public Transform contentTransform;

    private UIChallenge[] uiChallenges;

    private void Awake()
    {
        uiChallenges = new UIChallenge[GameManager.Instance.totalChallenge];

        for (int i = 0; i < uiChallenges.Length; i++)
        {
            uiChallenges[i] = Instantiate(uiChallengePrefab, contentTransform).GetComponent<UIChallenge>();
            uiChallenges[i].SetUp(i);
        }
    }
}