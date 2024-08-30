using UnityEngine;
using UnityEngine.SceneManagement;

public class BatteryData : MonoBehaviour
{
    private bool isGameOver = false; // Add this flag

    // Battery value
    private float batteryValue = 100f;

    // Duration for battery drain (5 minutes)
    public float totalTime = 300f;
    private float elapsedTime = 0f;

    // Singleton instance
    public static BatteryData Instance { get; private set; }

    void Awake()
    {
        // Ensure only one instance of BatteryData exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Update battery level over time
        if (elapsedTime < totalTime && !isGameOver)
        {
            elapsedTime += Time.deltaTime;
            batteryValue = Mathf.Lerp(100, 0, elapsedTime / totalTime);
        }
        else if (!isGameOver)
        {
            isGameOver = true; // Set the flag to prevent reloading
            SceneManager.LoadScene("End");
            enabled = false; // Disable this script to prevent further updates
        }
    }

    // Method to get the current battery value
    public float GetBatteryValue()
    {
        return batteryValue;
    }
}
