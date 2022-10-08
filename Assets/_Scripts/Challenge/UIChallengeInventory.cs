using UnityEngine;

public class UIChallengeInventory : MonoBehaviour
{
    public GameObject uiChallengePrefab;
    public Transform content;

    private void OnEnable()
    {
        this.ClearContent();

        foreach (Challenge challenge in GameManager.Instance.challenges)
        {
            this.CreateUIChallenge(challenge);
        }
    }

    private void CreateUIChallenge(Challenge challenge)
    {
        UIChallenge uiChallenge = Instantiate(uiChallengePrefab, content).GetComponent<UIChallenge>();
        uiChallenge.SetChallenge(challenge);
    }

    private void ClearContent()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}