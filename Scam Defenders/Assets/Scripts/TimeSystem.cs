using System.Collections;
using UnityEngine;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private int currentHour = 12; //Start time

    void Start()
    {
        //Coroutine will update the time every minute
        StartCoroutine(UpdateTime());
        UpdateTimeText();
    }

    IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
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
        timeText.text = currentHour.ToString("00") + ":00";
    }
}
