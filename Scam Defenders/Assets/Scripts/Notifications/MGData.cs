using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MGData : MonoBehaviour
{
    public static MGData Instance; //Singleton instance

    //Bools for tracking entered scenes
    public bool enteredScene1 = false; 
    public bool enteredScene2 = false; 
    public bool enteredScene3 = false; 
    public bool enteredScene4 = false; 

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
        //Update bools based on the loaded scene
        switch (scene.name)
        {
            case "ChatterNet":
                enteredScene1 = true;
                break;
            case "ShopEase":
                enteredScene2 = true;
                break;
            case "Geemail":
                enteredScene3 = true;
                break;
            case "QuickChats":
                enteredScene4 = true;
                break;
        }

        //Check if all 4 bools are true
        if (enteredScene1 && enteredScene2 && enteredScene3 && enteredScene4)
        {
            enteredScene1 = false;
            enteredScene2 = false;
            enteredScene3 = false;
            enteredScene4 = false;

            Debug.Log("All bools have been reset.");
        }
    }
}
