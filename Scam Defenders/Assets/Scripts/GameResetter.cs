using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResetter : MonoBehaviour
{
    // The name or index of your title screen scene
    [SerializeField] private string startSceneName = "Start";

    public void ResetGame()
    {
        // Find and destroy objects that should no longer persist across scenes
        DestroyPersistentObjects();

        // Reload the title screen scene
        SceneManager.LoadScene(startSceneName);
    }

    private void DestroyPersistentObjects()
    {
        // Example of finding objects by tag
        GameObject[] persistentObjects = GameObject.FindGameObjectsWithTag("Persistent");

        foreach (GameObject obj in persistentObjects)
        {
            Destroy(obj);
        }
    }
}
