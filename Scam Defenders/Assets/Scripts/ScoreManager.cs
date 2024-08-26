using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private bool ptsGiven = false;
    private PointChecker pointChecker;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Dynamically find the PointChecker instance if not already found
        if (pointChecker == null)
        {
            pointChecker = FindObjectOfType<PointChecker>();

            // If no PointChecker is found, exit the update early
            if (pointChecker == null)
            {
                return; // Safely handle scenes without a PointChecker
            }
        }

        if (pointChecker.IsWinActive && !ptsGiven)
        {
            RepData.Instance.PlusReputation(10);
            ptsGiven = true;
        }
        else if (pointChecker.IsLoseActive && !ptsGiven)
        {
            RepData.Instance.MinusReputation(10);
            ptsGiven = true;
        }

        // Reset the `ptsGiven` flag when neither win nor lose screen is active
        if (!pointChecker.IsWinActive && !pointChecker.IsLoseActive)
        {
            ptsGiven = false;
        }
    }
}
