using UnityEngine;

public class PointChecker : MonoBehaviour
{
    public GameObject[] winScreens;
    public GameObject[] loseScreens;

    public bool IsAnyWinActive { get; private set; }
    public bool IsAnyLoseActive { get; private set; }

    void Update()
    {
        IsAnyWinActive = CheckIfAnyActive(winScreens);
        IsAnyLoseActive = CheckIfAnyActive(loseScreens);
    }

    private bool CheckIfAnyActive(GameObject[] screens)
    {
        foreach (GameObject screen in screens)
        {
            if (screen.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
