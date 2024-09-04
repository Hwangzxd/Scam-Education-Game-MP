using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] guides;
    private int currentGuideIndex = 0;
    private static bool isTutSeen = false; // Static variable persists across scenes
    public GameObject shopeaseSkipBtn;
    public string[] shopeaseScenes;

    public ChatManager chatManager;

    void Start()
    {
        chatManager.PauseChat();

        Debug.Log("Start method called. isTutSeen: " + isTutSeen);

        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentSceneName);

        if (IsSceneInList(currentSceneName, shopeaseScenes))
        {
            HandleSkipButton(shopeaseSkipBtn);
        }

        for (int i = 0; i < guides.Length; i++)
        {
            guides[i].SetActive(i == 0);
            Debug.Log("Guide " + i + " active status: " + guides[i].activeSelf);
        }
    }

    private void HandleSkipButton(GameObject skipButton)
    {
        if (skipButton != null)
        {
            skipButton.SetActive(false);

            if (isTutSeen == false)
            {
                Debug.Log("Tutorial not seen before. Setting isTutSeen to true.");
                isTutSeen = true;
            }
            else
            {
                Debug.Log("Tutorial already seen. Activating skip button.");
                skipButton.SetActive(true);
            }
        }
    }

    private bool IsSceneInList(string sceneName, string[] sceneList)
    {
        foreach (string name in sceneList)
        {
            if (sceneName == name)
            {
                return true;
            }
        }
        return false;
    }

    public void EnableNextGuide()
    {
        Debug.Log("EnableNextGuide method called. Current Guide Index: " + currentGuideIndex);

        if (currentGuideIndex < guides.Length)
        {
            guides[currentGuideIndex].SetActive(false);
            currentGuideIndex++;

            if (currentGuideIndex < guides.Length)
            {
                guides[currentGuideIndex].SetActive(true);
            }
            else
            {
                chatManager.ResumeChat();
            }
        }
    }

    public void OnTutorialSkipped()
    {
        chatManager.ResumeChat();
    }
}
