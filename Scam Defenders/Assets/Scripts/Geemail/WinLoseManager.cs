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
        // Reference GMData instance
        if (GMData.Instance != null)
        {
            // Get current score from GMData
            int score = GMData.Instance.GetScore(); // Change to int if GMData returns int

            Debug.Log("Current Score: " + score); // Debug log

            if (score < 2)
            {
                // Activate lose pop-up and deactivate win pop-up
                losePopUp.SetActive(true);
                winPopUp.SetActive(false);
                Debug.Log("Lose pop-up activated."); // Debug log
            }
            else if (score >= 2 && score <= 3)
            {
                // Activate win pop-up and deactivate lose pop-up
                winPopUp.SetActive(true);
                losePopUp.SetActive(false);
                Debug.Log("Win pop-up activated."); // Debug log
            }
        }
        else
        {
            Debug.LogWarning("GMData instance is not found. Make sure it is added to the scene.");
        }
    }
}
