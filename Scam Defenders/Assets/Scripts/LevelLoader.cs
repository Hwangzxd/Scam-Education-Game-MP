using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public static LevelLoader instance; 

    public GameObject levelLoader; 

    public GameObject afterMailMG;
    public GameObject flagHolder;

    public float transitionTime = 1f;

    private Scene currentScene;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Make sure levelLoader is not destroyed
            if (levelLoader != null)
            {
                DontDestroyOnLoad(levelLoader.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // For testing purposes, you can trigger level loading with a key press
        // if (Input.GetMouseButtonDown(0))
        // {
        //     LoadNextLevel();
        // }
    }

    public void LoadNextLevel()
    {
        currentScene = SceneManager.GetActiveScene();

        // Replace "ShopEase" and "GeemailMG" with the actual scene names or conditions
        if (currentScene.name == "ShopEase")
        {
            StartCoroutine(LoadLevel("ShopEaseChoose"));
        }
        else if (currentScene.name == "GeemailMG")
        {
            StartCoroutine(MailCanvasOn());
        }
    }

    IEnumerator LoadLevel(string sceneName)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait for the animation to finish
        yield return new WaitForSeconds(transitionTime);

        // Load the specified scene
        SceneManager.LoadScene(sceneName);

        // Optionally destroy the levelLoader GameObject after the scene has loaded
        Destroy(levelLoader);
    }

    IEnumerator MailCanvasOn()
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait for the animation to finish
        yield return new WaitForSeconds(transitionTime);

        // Activate the mail canvas and deactivate the flag holder
        afterMailMG.SetActive(true);
        flagHolder.SetActive(false);

        // Optionally destroy the levelLoader GameObject after the scene has loaded
        Destroy(levelLoader); 
    }
}
