using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MGData : MonoBehaviour
{
    public static MGData Instance; // Singleton instance

    public enum Scenes
    {
        ChatterNet,
        ShopEase,
        Geemail,
        YabberChatHome
    }

    public HashSet<Scenes> enteredScenes = new HashSet<Scenes>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene matches any in the enum
        if (System.Enum.TryParse(scene.name, out Scenes sceneEnum))
        {
            enteredScenes.Add(sceneEnum);
        }

        // Check if all scenes have been entered
        if (enteredScenes.Count == System.Enum.GetValues(typeof(Scenes)).Length)
        {
            enteredScenes.Clear();
            Debug.Log("All scenes have been entered and the set has been reset.");
        }
    }
}
