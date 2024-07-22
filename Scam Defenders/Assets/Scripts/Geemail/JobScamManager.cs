using UnityEngine;
using TMPro;

public class JobScamManager : MonoBehaviour
{
    public DropArea[] scamSigns;
    public TextMeshProUGUI statusText; // Reference to the TextMeshPro component

    void Start()
    {
        if (scamSigns == null || scamSigns.Length == 0)
        {
            scamSigns = FindObjectsOfType<DropArea>();
        }

        UpdateStatusText(0); // Initialize the status text
    }

    void Update()
    {
        GameStatus();
    }

    private void GameStatus()
    {
        int foundSignsCount = 0;

        // Check each scam sign to see if it's occupied
        foreach (DropArea dropArea in scamSigns)
        {
            if (dropArea.isOccupied)
            {
                foundSignsCount++;
            }
        }

        // Provide feedback based on the number of signs found
        UpdateStatusText(foundSignsCount);
    }

    private void UpdateStatusText(int foundSignsCount)
    {
        switch (foundSignsCount)
        {
            case 0:
                statusText.text = "0/3 signs";
                break;
            case 1:
                statusText.text = "1/3 signs";
                break;
            case 2:
                statusText.text = "2/3 signs";
                break;
            case 3:
                statusText.text = "3/3 signs";
                break;
        }
    }
}
