using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem instance;
    public TextMeshProUGUI timeText;

    private int currentHour = 12; // Start time
    private int currentMinute = 0;

    private const float totalRealTime = 300f; // 5 minutes in seconds
    private const int totalGameMinutes = 2 * 60; // 2 hours in game minutes
    private const float incrementInterval = 10f; // Every 10 seconds
    private const float minutesPerInterval = totalGameMinutes / (totalRealTime / incrementInterval);

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Make sure timeText is not destroyed
            if (timeText != null)
            {
                DontDestroyOnLoad(timeText.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Start the coroutine to update the time every minute
        StartCoroutine(UpdateTime());
        UpdateTimeText();
    }

    IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(incrementInterval); // Wait for 10 seconds
            IncrementTime();
            UpdateTimeText();

            // Check if the game time has reached 2 PM
            if (currentHour == 14 && currentMinute == 0)
            {
                EndGame();
                yield break; // Stop the coroutine
            }
        }
    }

    void IncrementTime()
    {
        // Increment time by the calculated minutes per interval
        currentMinute += Mathf.FloorToInt(minutesPerInterval);

        // Handle overflow of minutes
        if (currentMinute >= 60)
        {
            currentHour++;
            currentMinute -= 60;
        }

        // Handle overflow of hours
        if (currentHour > 23)
        {
            currentHour = 0;
        }
    }

    void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = currentHour.ToString("00") + ":" + currentMinute.ToString("00");
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over! Reached 2 PM.");
        SceneManager.LoadScene("End");
    }
}
