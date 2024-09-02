using UnityEngine;
using UnityEngine.UI;

public class RepBarData : MonoBehaviour
{
    public Slider reputationSlider;  // Reference to the UI Slider component for the reputation bar
    public Image reputationSprite;   // Reference to the Image component for the reputation sprite
    public Sprite[] reputationSprites; // Array to store the different reputation sprites (0 to 100, incremented by 10)

    private RepData repData; // Reference to the singleton instance of RepData

    void Start()
    {
        // Access the RepData singleton instance
        repData = RepData.Instance;

        if (repData != null && reputationSlider != null && reputationSprite != null && reputationSprites.Length == 11)
        {
            // Initialize the slider with the current reputation value
            InitializeSlider();
            UpdateSlider();
            UpdateReputationSprite();
        }
        else
        {
            Debug.LogError("RepData instance, ReputationSlider, ReputationSprite, or ReputationSprites not properly set.");
        }
    }

    void Update()
    {
        if (repData != null && reputationSlider != null)
        {
            // Update the slider value and sprite if the reputation changes
            UpdateSlider();
            UpdateReputationSprite();
        }
    }

    void InitializeSlider()
    {
        // Set the slider's min and max values based on RepData
        reputationSlider.minValue = repData.minRep;
        reputationSlider.maxValue = repData.maxRep;
    }

    void UpdateSlider()
    {
        // Set the slider's value to the current reputation value
        reputationSlider.value = repData.GetReputation();
    }

    void UpdateReputationSprite()
    {
        float reputation = repData.GetReputation();

        // Calculate the index based on the percentage of reputation
        int spriteIndex = Mathf.FloorToInt(reputation / 10.0f);

        // Ensure the index is within the bounds of the array
        if (spriteIndex >= 0 && spriteIndex < reputationSprites.Length)
        {
            reputationSprite.sprite = reputationSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Reputation sprite index out of bounds.");
        }
    }
}
