using UnityEngine;

public class WinLoseManager : MonoBehaviour
{
    public GameObject winPopUp;
    public GameObject losePopUp;

    void Start()
    {
        CheckScoreAndActivatePopUp();
    }

    void CheckScoreAndActivatePopUp()
    {
        int score = ScoreManager.instance.GetJobScamScore();

        if (score < 2)
        {
            // Activate lose pop-up and deactivate win pop-up
            losePopUp.SetActive(true);
            winPopUp.SetActive(false);

            // Reference RepData instance and decrease reputation
            if (RepData.Instance != null)
            {
                RepData.Instance.MinusReputation(10);
            }
        }
        else if (score >= 2 && score <= 3)
        {
            // Activate win pop-up and deactivate lose pop-up
            winPopUp.SetActive(true);
            losePopUp.SetActive(false);

            // Reference RepData instance and increase reputation
            if (RepData.Instance != null)
            {
                RepData.Instance.PlusReputation(10);
            }
        }
    }
}
