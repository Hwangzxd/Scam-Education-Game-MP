using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class JobScamManager : MonoBehaviour
{
    public DropArea[] scamSigns;
    public int score = 0; // Initialize score 

    public static JobScamManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Remove persistence by commenting out or removing the following line:
            // DontDestroyOnLoad(gameObject); // Ensure this object persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
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

        // Check if the scene is the start screen and reset score if necessary
        if (scene.name == "StartScreen") // Replace "StartScreen" with your actual start screen scene name
        {
            ResetScore();
        }
    }

    private void InitializeDropAreas()
    {
        scamSigns = FindObjectsOfType<DropArea>(); // Find DropAreas in the current scene

        if (scamSigns.Length == 0)
        {
            Debug.LogWarning("No DropArea instances found in the current scene.");
        }

        UpdateStatusText(0); // Update status text with initial values
    }

    void Update()
    {
        GameStatus();
    }

    private void GameStatus()
    {
        int foundSignsCount = 0;

        // Check each scam sign to see if occupied
        foreach (DropArea dropArea in scamSigns)
        {
            if (dropArea.isOccupied)
            {
                foundSignsCount++;
            }
        }

        UpdateStatusText(foundSignsCount);
        UpdateGMData(); // Update GMData with the current score
    }

    private void UpdateStatusText(int foundSignsCount)
    {
        switch (foundSignsCount)
        {
            case 0:
                score = 0; // Set score to 0
                break;
            case 1:
                score = 1; // Set score to 1
                break;
            case 2:
                score = 2; // Set score to 2
                break;
            case 3:
                score = 3; // Set score to 3
                break;
        }
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
