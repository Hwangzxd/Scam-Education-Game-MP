using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneHistoryManager : MonoBehaviour
{
    private Stack<string> sceneHistory = new Stack<string>();

    private static SceneHistoryManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static SceneHistoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("SceneHistoryManager");
                instance = obj.AddComponent<SceneHistoryManager>();
            }
            return instance;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentSceneName = scene.name;
        if (sceneHistory.Count == 0 || sceneHistory.Peek() != currentSceneName)
        {
            sceneHistory.Push(currentSceneName);
        }
    }

    public void Back()
    {
        if (sceneHistory.Count > 1)
        {
            sceneHistory.Pop();
            string previousScene = sceneHistory.Peek();
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("No previous scene in history.");
        }
    }

    public void ResetHistory()
    {
        sceneHistory.Clear();

        // Add the current scene to the history after reset
        string currentSceneName = SceneManager.GetActiveScene().name;
        sceneHistory.Push(currentSceneName);
    }
}
