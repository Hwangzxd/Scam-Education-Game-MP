using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public JobScamManager jobScamManager; // Reference to JobScamManager
    public int jobScamScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Check if the JobScamManager reference is set and if it's not null
        if (jobScamManager != null)
        {
            // Get the current score from the JobScamManager
            jobScamScore = jobScamManager.GetScore();
        }
        else
        {
            // Optionally, try to find the JobScamManager in the new scene
            JobScamManager foundManager = FindObjectOfType<JobScamManager>();

            if (foundManager != null)
            {
                jobScamManager = foundManager;
            }
            else
            {
                // If JobScamManager doesn't exist in the new scene, do nothing or handle accordingly
                // You could log a warning, or simply continue without updating the score
            }
        }
    }

    public int GetJobScamScore()
    {
        return jobScamScore;
    }
}