using UnityEngine;

public class PointChecker : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    public bool IsWinActive { get; private set; }
    public bool IsLoseActive { get; private set; }

    void Update()
    {
        IsWinActive = winScreen.activeSelf;
        IsLoseActive = loseScreen.activeSelf;
    }
}
