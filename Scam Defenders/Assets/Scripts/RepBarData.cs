using UnityEngine;
using UnityEngine.UI;

public class RepBarData : MonoBehaviour
{
    public Slider reputationSlider; 
    private RepData repData; 

    void Start()
    {
        // Initialize the RepData instance
        repData = RepData.Instance;

        if (repData != null && reputationSlider != null)
        {
            // Initialize the slider with the current reputation value
            UpdateSlider();
        }
    }

    void Update()
    {
        if (repData != null && reputationSlider != null)
        {
            // Update the slider value if the reputation changes
            UpdateSlider();
        }
    }

    void UpdateSlider()
    {
        // Set the slider's value to the current reputation value
        reputationSlider.value = repData.GetReputation();
        reputationSlider.maxValue = repData.maxRep;
        reputationSlider.minValue = repData.minRep;
    }
}
