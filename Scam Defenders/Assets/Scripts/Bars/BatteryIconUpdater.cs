using UnityEngine;
using UnityEngine.UI;

public class BatteryIconUpdater : MonoBehaviour
{
    // Array to store battery icon images
    public Sprite[] batteryIcons;

    // Reference to the UI Image component for the battery icon at the top of the screen
    public Image topBatteryIconImage;

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

        // Initial update of the battery icon
        UpdateBatteryIcon(batteryData.GetBatteryValue());
    }

    void Update()
    {
        // Update battery icon based on BatteryData
        UpdateBatteryIcon(batteryData.GetBatteryValue());
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

        topBatteryIconImage.sprite = newBatteryIcon;
    }
}
