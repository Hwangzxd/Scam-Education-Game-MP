using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
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

    public void GoToQuickChats()
    {
        SceneManager.LoadScene("QuickChats");
    }

    public void GoToChats()
    {
        SceneManager.LoadScene("Chats");
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
    } 
     
    public void GoToGeeMail()
    {
        SceneManager.LoadScene("Geemail");
    }
}
