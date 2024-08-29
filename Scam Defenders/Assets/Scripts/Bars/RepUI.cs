using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class RepUI : MonoBehaviour
{
    public TextMeshProUGUI repText; // Reference to the TextMeshProUGUI component

    void Start()
    {
        // Update the text at the start
        UpdateReputationText();
    }

    void OnEnable()
    {
        // Subscribe to updates every frame
        RepData.Instance.RepValueChanged += UpdateReputationText;
    }

    void OnDisable()
    {
        // Unsubscribe when the object is disabled to prevent memory leaks
        RepData.Instance.RepValueChanged -= UpdateReputationText;
    }

    // Method to update the reputation text
    public void UpdateReputationText()
    {
        repText.text = "Reputation: " + RepData.Instance.GetReputation().ToString();
    }
}
