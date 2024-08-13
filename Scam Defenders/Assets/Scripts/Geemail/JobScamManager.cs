using UnityEngine;
using TMPro;

public class JobScamManager : MonoBehaviour
{
    public DropArea[] scamSigns;
    public TextMeshProUGUI statusText;
    public int score = 0; // Initialize score

    void Start()
    {
        if (scamSigns == null || scamSigns.Length == 0)
        {
            scamSigns = FindObjectsOfType<DropArea>();
        }

        UpdateStatusText(0);
    }

    void Update()
    {
        GameStatus();
    }

    private void GameStatus()
    {
        int foundSignsCount = 0;

        // Check each scam sign to see if occupied
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
                score = 0; // Set score to 0
                statusText.text = "0/3 signs - Score: " + score;
                break;
            case 1:
                score = 1; // Set score to 1
                statusText.text = "1/3 signs - Score: " + score;
                break;
            case 2:
                score = 2; // Set score to 2
                statusText.text = "2/3 signs - Score: " + score;
                break;
            case 3:
                score = 3; // Set score to 3
                statusText.text = "3/3 signs - Score: " + score;
                break;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
