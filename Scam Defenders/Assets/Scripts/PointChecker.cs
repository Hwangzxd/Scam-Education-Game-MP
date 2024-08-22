using UnityEngine;

public class PointChecker : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    void Update()
    {
        if (winScreen.activeSelf)
        {
            GMData.Instance.SetWin(true);
            GMData.Instance.SetLose(false);
            Debug.Log("Win screen is active.");
        }
        else if (loseScreen.activeSelf)
        {
            GMData.Instance.SetWin(false);
            GMData.Instance.SetLose(true);
            Debug.Log("Lose screen is active.");
        }
    }
}
