using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BatteryBar : MonoBehaviour
{
    // Reference to the Slider component for battery display
    public Slider slider;

    // Array to store battery icon images
    public Sprite[] batteryIcons;

    // Reference to the UI Image component for the battery icon on the slider
    public Image batteryIconImage;

    // Reference to the UI Image component for the battery icon at the top of the screen
    public Image topBatteryIconImage;

    // Reference to the UI Text component for the battery percentage
    public TextMeshProUGUI batteryPercentageText;

    private BatteryData batteryData;

    // Start is called before the first frame update
    void Start()
    {
        // Access the BatteryData singleton instance
        batteryData = BatteryData.Instance;

        if (batteryData == null)
        {
            Debug.LogError("BatteryData instance not found.");
            return;
        }

        // Set max battery level based on BatteryData
        SetMaxBatteryLevel(100);
    }

    // Update is called once per frame
    void Update()
    {
        // Update battery visuals based on BatteryData
        UpdateBatteryVisuals();
    }

    // Sets max value of battery bar and also the current value
    public void SetMaxBatteryLevel(int value)
    {
        slider.maxValue = value;
        slider.value = batteryData.GetBatteryValue();
    }

    // Method to update the battery visuals based on the battery data
    private void UpdateBatteryVisuals()
    {
        if (batteryData == null) return;

        float batteryLevel = batteryData.GetBatteryValue();
        slider.value = batteryLevel;
        UpdateBatteryIcon(batteryLevel);
        UpdateBatteryPercentageText(batteryLevel);
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
