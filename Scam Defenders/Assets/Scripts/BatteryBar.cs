using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    //gets the Slider component and calls it slider
    public Slider slider;

    //total time for the battery to drain in seconds (5 minutes)
    private float totalTime = 300f;

    //start is called before the first frame update
    void Start()
    {
        //set max battery level to 100 and also sets it to 100
        SetMaxBatteryLevel(100);
        //start the battery drain coroutine
        StartCoroutine(DrainBattery());
    }

    //sets max value of battery bar and also the current value
    public void SetMaxBatteryLevel(int value)
    {
        //changes the slider's max value
        slider.maxValue = value;

        //changes the slider's current value
        slider.value = value;
    }

    //coroutine to drain the battery over time
    private IEnumerator DrainBattery()
    {
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            //calculate the new battery level
            float newBatteryLevel = Mathf.Lerp(slider.maxValue, 0, elapsedTime / totalTime);
            //set the new battery level
            slider.value = newBatteryLevel;
            //increment elapsed time
            elapsedTime += Time.deltaTime;
            //wait for the next frame
            yield return null;
        }

        //set battery level to 0 when time is up
        slider.value = 0;
        //trigger event when the battery is completely drained
        OnBatteryDrained();
    }

    //method to handle the event when battery is drained
    private void OnBatteryDrained()
    {
        Debug.Log("Battery is completely drained.");
    }

    //method to get the current battery level
    public float GetBatteryLevel()
    {
        return slider.value;
    }
}
