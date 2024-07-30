using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class RealTimeClock : MonoBehaviour
{
    public static RealTimeClock instance;
    public TextMeshProUGUI timeText;

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

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

    void Update()
    {
        // Get the current time
        DateTime now = DateTime.Now;

        // Format the time string
        string timeString = now.ToString("h:mm tt"); // Change the format as needed

        // Update the UI text
        timeText.text = timeString;
    }
}