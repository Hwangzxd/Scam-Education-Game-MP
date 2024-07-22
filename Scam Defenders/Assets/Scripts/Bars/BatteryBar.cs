using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BatteryBar : MonoBehaviour
{
    // Gets the Slider component and calls it slider
    public Slider slider;

    // Total time for the battery to drain in seconds (5 minutes)
    private float totalTime = 300f;

    // Array to store battery icon images
    public Sprite[] batteryIcons;

    // Reference to the UI Image component for the battery icon on the slider
    public Image batteryIconImage;

    // Reference to the UI Image component for the battery icon at the top of the screen
    public Image topBatteryIconImage;

    // Reference to the UI Text component for the battery percentage
    public TextMeshProUGUI batteryPercentageText;

    // Start is called before the first frame update
    void Start()
    {
        // Set max battery level to 100 and also sets it to 100
        SetMaxBatteryLevel(100);
        // Start the battery drain coroutine
        StartCoroutine(DrainBattery());
    }

    // Sets max value of battery bar and also the current value
    public void SetMaxBatteryLevel(int value)
    {
        // Changes the slider's max value
        slider.maxValue = value;

        // Changes the slider's current value
        slider.value = value;
    }

    // Coroutine to drain the battery over time
    private IEnumerator DrainBattery()
    {
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            // Calculate the new battery level
            float newBatteryLevel = Mathf.Lerp(slider.maxValue, 0, elapsedTime / totalTime);
            // Set the new battery level
            slider.value = newBatteryLevel;
            // Update the battery icons
            UpdateBatteryIcon(newBatteryLevel);
            // Update the battery percentage text
            UpdateBatteryPercentageText(newBatteryLevel);
            // Increment elapsed time
            elapsedTime += Time.deltaTime;
            // Wait for the next frame
            yield return null;
        }

        // Set battery level to 0 when time is up
        slider.value = 0;
        // Trigger event when the battery is completely drained
        OnBatteryDrained();
    }

    // Method to handle the event when battery is drained
    private void OnBatteryDrained()
    {
        Debug.Log("Battery is completely drained.");
    }

    // Method to get the current battery level
    public float GetBatteryLevel()
    {
        return slider.value;
    }

    // Method to update the battery icons based on the battery level
    private void UpdateBatteryIcon(float batteryLevel)
    {
        Sprite newBatteryIcon;

        if (batteryLevel > 80)
        {
            newBatteryIcon = batteryIcons[0];
        }
        else if (batteryLevel > 60)
        {
            newBatteryIcon = batteryIcons[1];
        }
        else if (batteryLevel > 40)
        {
            newBatteryIcon = batteryIcons[2];
        }
        else if (batteryLevel > 20)
        {
            newBatteryIcon = batteryIcons[3];
        }
        else
        {
            newBatteryIcon = batteryIcons[4];
        }

        // Update both battery icon images
        batteryIconImage.sprite = newBatteryIcon;
        topBatteryIconImage.sprite = newBatteryIcon;
    }

    // Method to update the battery percentage text
    private void UpdateBatteryPercentageText(float batteryLevel)
    {
        batteryPercentageText.text = Mathf.RoundToInt(batteryLevel) + "%";
    }
}
