using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class RealTimeClock : MonoBehaviour
{
    public TextMeshProUGUI timeText;

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