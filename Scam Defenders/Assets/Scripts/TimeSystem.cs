using System.Collections;
using UnityEngine;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem instance;
    public TextMeshProUGUI timeText;

    private int currentHour = 12; // Start time

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
            yield return new WaitForSeconds(60); // Wait for 1 minute 
            IncrementTime();
            UpdateTimeText();
        }
    }

    void IncrementTime()
    {
        currentHour++;
        if (currentHour > 23)
        {
            currentHour = 0;
        }
    }

    void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = currentHour.ToString("00") + ":00";
        }
    }
}
