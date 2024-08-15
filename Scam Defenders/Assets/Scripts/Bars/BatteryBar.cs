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

    // Reference to the UI Text component for the battery percentage
    public TextMeshProUGUI batteryPercentageText;

    private BatteryData batteryData;

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

    void Update()
    {
        // Update battery visuals based on BatteryData
        UpdateBatteryVisuals();
    }

    public void SetMaxBatteryLevel(int value)
    {
        slider.maxValue = value;
        slider.value = batteryData.GetBatteryValue();
    }

    private void UpdateBatteryVisuals()
    {
        if (batteryData == null) return;

        float batteryLevel = batteryData.GetBatteryValue();
        slider.value = batteryLevel;
        UpdateBatteryIcon(batteryLevel);
        UpdateBatteryPercentageText(batteryLevel);
    }

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

        batteryIconImage.sprite = newBatteryIcon;
    }

    private void UpdateBatteryPercentageText(float batteryLevel)
    {
        batteryPercentageText.text = Mathf.RoundToInt(batteryLevel) + "%";
    }
}
