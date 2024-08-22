using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private JobScamManager jobScamManager; // Private reference to JobScamManager
    private int jobScamScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes

            // Automatically find the JobScamManager instance
            jobScamManager = FindObjectOfType<JobScamManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Check if the JobScamManager reference is set and not null
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
        }
    }

    public int GetJobScamScore()
    {
        return jobScamScore;
    }
}
