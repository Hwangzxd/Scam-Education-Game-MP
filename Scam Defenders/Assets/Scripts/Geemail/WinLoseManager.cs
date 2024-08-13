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
            losePopUp.SetActive(true);
            winPopUp.SetActive(false);
        }
        else if (score >= 2 && score <= 3)
        {
            winPopUp.SetActive(true);
            losePopUp.SetActive(false);
        }
    }
}
