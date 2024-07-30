using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void LoadNextScene()
    {
        //Gets the current active scene + Calculates next scene index
        Scene currentScene = SceneManager.GetActiveScene();
        int nextSceneIndex = currentScene.buildIndex + 1;

        //Checks if the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            //Loads the next scene
            SceneManager.LoadScene(nextSceneIndex);
            audioManager.PlaySFX(audioManager.ButtonSFX);
        }
        else
        {
            Debug.LogWarning("No more scenes in the build settings.");
        }
    }

    public void LoadLastScene()
    {
        //Gets the current active scene + Calculates last scene index
        Scene currentScene = SceneManager.GetActiveScene();
        int lastSceneIndex = currentScene.buildIndex - 1;

        //Checks if the next scene index is within the valid range
        if (lastSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            //Loads the next scene
            SceneManager.LoadScene(lastSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes in the build settings.");
        }
    } 
     
    public void GoToHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void GoToHome2()
    {
        SceneManager.LoadScene("Home2");
    }

    public void GoToYabberChatChat()
    {
        SceneManager.LoadScene("YabberChatChat");
    }

    public void GoToYabberChatHome()
    {
        SceneManager.LoadScene("YabberChatHome");
    } 
     
    public void GoToChatterNet()
    {
        SceneManager.LoadScene("ChatterNet");
    } 
     
    public void GoToChatterNetMessages()
    {
        SceneManager.LoadScene("ChatterNetMessages");
    } 
     
    public void GoToLoveScamChat()
    {
        SceneManager.LoadScene("LoveScamChat");
    }

    public void GoToStart()
    {
        SceneManager.LoadScene("Start");
        audioManager.PlaySFX(audioManager.ButtonSFX);
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings");
        audioManager.PlaySFX(audioManager.ButtonSFX);
    }

    public void GoToGeeMail()
    {
        SceneManager.LoadScene("Geemail");
    }

    public void GoToGeeMailMG()
    {
        SceneManager.LoadScene("GeemailMG");
    }


    public void GoToNotifications()
    {
        SceneManager.LoadScene("Notifications");
    }

    public void GoToShopEaseSearch()
    {
        SceneManager.LoadScene("ShopEaseSearch");
    }

    public void GoToShopEaseChat()
    {
        SceneManager.LoadScene("ShopEaseChat");
    }

    public void GoToShopEaseProfile1()
    {
        SceneManager.LoadScene("ShopEaseProfile1");
    }

    public void GoToShopEaseProfile2()
    {
        SceneManager.LoadScene("ShopEaseProfile2");
    }

    public void GoToShopEase()
    {
        SceneManager.LoadScene("ShopEase");
    }

}
