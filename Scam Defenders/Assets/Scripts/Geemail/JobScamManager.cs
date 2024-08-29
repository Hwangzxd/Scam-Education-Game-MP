using UnityEngine;
using UnityEngine.SceneManagement;

public class JobScamManager : MonoBehaviour
{
    [System.Serializable]
    public class DropAreaScore
    {
        public DropArea dropArea;
        public int scoreValue;
    }

    public DropAreaScore[] scamSignsAndScores;
    public int score = 0;

    public static JobScamManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeDropAreas();
        UpdateGMData(); // Initialize GMData with the current score
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to sceneLoaded event
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from sceneLoaded event
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        InitializeDropAreas(); // Reinitialize DropAreas when a new scene is loaded
        UpdateGMData(); // Update GMData with the current score when the scene is loaded

        if (scene.name == "StartScreen") // Replace "StartScreen" with your actual start screen scene name
        {
            ResetScore();
        }
    }

    private void InitializeDropAreas()
    {
        // This assumes that the DropArea references are already set in the Inspector
        if (scamSignsAndScores.Length == 0)
        {
            Debug.LogWarning("No DropAreaScore instances found in the current scene.");
        }

        UpdateStatusText(); // Update status text with initial values
    }

    void Update()
    {
        GameStatus();
    }

    private void GameStatus()
    {
        int totalScore = 0;

        // Check each scam sign to see if occupied
        foreach (DropAreaScore item in scamSignsAndScores)
        {
            if (item.dropArea.isOccupied)
            {
                totalScore += item.scoreValue; // Add the score value of the occupied DropArea
            }
        }

        score = totalScore; // Update the score
        UpdateStatusText();
        UpdateGMData(); // Update GMData with the current score
    }

    private void UpdateStatusText()
    {
        // Implement any UI update logic here based on the score
    }

    private void UpdateGMData()
    {
        if (GMData.Instance != null)
        {
            GMData.Instance.SetScore(score); // Set the score in GMData
            Debug.Log("JobScamManager Score Updated: " + score); // Debug log
        }
        else
        {
            Debug.LogError("GMData Instance is null."); // Error log if GMData is not found
        }
    }

    private void ResetScore()
    {
        score = 0; // Reset the score
        UpdateGMData(); // Update GMData with the reset score
        Debug.Log("Score reset on Start Screen."); // Debug log
    }

    public int GetScore()
    {
        return score;
    }
}
