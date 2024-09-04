using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRandomizer : MonoBehaviour
{
    public string[] shopEaseScenes = { "ShopEase", "ShopEase2", "ShopEase3" };
    public string[] birdyNetScenes = { "LoveScamChatNew1", "LoveScamChatNew2", "LoveScamChatNew3" };
    public string[] yabberChatINVSScenes = { "YabberChatINVS", "YabberChatINVS1", "YabberChatINVS2" };
    public string[] yabberChatGOISScenes = { "YabberChatGOIS1", "YabberChatGOIS2" };

    private List<string> shownShopEaseScenes = new List<string>();
    private List<string> shownBirdyNetScenes = new List<string>();
    private List<string> shownINVSScenes = new List<string>();
    private List<string> shownGOISScenes = new List<string>();

    public void LoadRandomSceneShopEase()
    {
        LoadRandomScene(shopEaseScenes, shownShopEaseScenes);
    }

    public void LoadRandomSceneBirdyNet()
    {
        LoadRandomScene(birdyNetScenes, shownBirdyNetScenes);
    }

    public void LoadRandomSceneINVS()
    {
        LoadRandomScene(yabberChatINVSScenes, shownINVSScenes);
    }

    public void LoadRandomSceneGOIS()
    {
        LoadRandomScene(yabberChatGOISScenes, shownGOISScenes);
    }

    private void LoadRandomScene(string[] scenes, List<string> shownScenes)
    {
        // Check if all scenes have been shown
        if (shownScenes.Count >= scenes.Length)
        {
            Debug.Log("All scenes have been shown. Resetting shown scenes.");
            // Clear the list to allow re-shuffling
            shownScenes.Clear();
        }

        List<string> availableScenes = new List<string>(scenes);
        availableScenes.RemoveAll(scene => shownScenes.Contains(scene));

        if (availableScenes.Count == 0)
        {
            Debug.LogWarning("No available scenes to load.");
            return;
        }

        int randomIndex = Random.Range(0, availableScenes.Count);
        string selectedScene = availableScenes[randomIndex];

        // Add the selected scene to the list of shown scenes
        shownScenes.Add(selectedScene);

        Debug.Log($"Loading Scene: {selectedScene} (Index: {randomIndex})");
        SceneManager.LoadScene(selectedScene);
    }
}
