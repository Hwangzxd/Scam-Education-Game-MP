using System.Collections.Generic;
using UnityEngine;

public class MGRandomizer : MonoBehaviour
{
    public List<GameObject> miniGames; // List to hold the mini-game GameObjects

    private void Start()
    {
        RandomizeAndActivateMiniGame();
    }

    // Method to randomize and activate a mini-game
    private void RandomizeAndActivateMiniGame()
    {
        if (miniGames.Count > 0)
        {
            // Deactivate all mini-games first
            DeactivateAll();

            // Randomly select and activate one mini-game
            int randomIndex = Random.Range(0, miniGames.Count);
            miniGames[randomIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("No mini-games available in the list.");
        }
    }

    // Method to deactivate all mini-games
    private void DeactivateAll()
    {
        foreach (GameObject miniGame in miniGames)
        {
            miniGame.SetActive(false);
        }
    }
}
