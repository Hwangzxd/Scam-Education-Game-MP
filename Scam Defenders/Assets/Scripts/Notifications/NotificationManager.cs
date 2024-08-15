using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotificationManager : MonoBehaviour
{
    public GameObject[] notifs;
    public GameObject[] pings;
    private List<int> shownNotifs = new List<int>();

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdateNotifs();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateNotifs();
    }

    private void UpdateNotifs()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        MGData data = MGData.Instance;

        if (data == null)
        {
            Debug.LogError("MGData instance is missing.");
            return;
        }

        if (currentScene == "Home")
        {
            BlockNotifs(data);
            ShowRandomNotif();
        }
        else
        {
            foreach (GameObject notif in notifs)
            {
                notif.SetActive(false);
            }

            foreach (GameObject ping in pings)
            {
                ping.SetActive(false);
            }
        }
    }

    private void ShowRandomNotif()
    {
        if (notifs.Length == 0)
        {
            Debug.LogWarning("No notifs assigned.");
            return;
        }

        List<int> availableNotifs = new List<int>();

        for (int i = 0; i < notifs.Length; i++)
        {
            // Check if the notif is inactive & not blocked by MGData
            if (!shownNotifs.Contains(i) && !notifs[i].activeSelf && !IsNotificationBlocked(i))
            {
                availableNotifs.Add(i);
            }
        }

        // If all notifs have been shown, reset the list
        if (availableNotifs.Count == 0)
        {
            shownNotifs.Clear();
            for (int i = 0; i < notifs.Length; i++)
            {
                // Check again if the notif is inactive & not blocked
                if (!notifs[i].activeSelf && !IsNotificationBlocked(i))
                {
                    availableNotifs.Add(i);
                }
            }
        }

        if (availableNotifs.Count == 0)
        {
            Debug.LogWarning("No available notifs to show.");
            return;
        }

        int randomIndex = availableNotifs[Random.Range(0, availableNotifs.Count)];
        shownNotifs.Add(randomIndex);

        notifs[randomIndex].SetActive(true);

        if (pings.Length > randomIndex)
        {
            pings[randomIndex].SetActive(true);
        }
    }

    private bool IsNotificationBlocked(int index)
    {
        MGData data = MGData.Instance;
        if (data == null)
        {
            Debug.LogError("MGData instance is missing.");
            return false;
        }

        // Map the index to the corresponding enum value
        MGData.Scenes sceneEnum = (MGData.Scenes)index;

        // Check if the scene has already been entered
        return data.enteredScenes.Contains(sceneEnum);
    }

    private void BlockNotifs(MGData data)
    {
        for (int i = 0; i < notifs.Length; i++)
        {
            MGData.Scenes sceneEnum = (MGData.Scenes)i;
            if (data.enteredScenes.Contains(sceneEnum))
            {
                DisableNotification(i); // Disable notification for the corresponding scene
            }
        }
    }

    private void DisableNotification(int index)
    {
        if (index >= 0 && index < notifs.Length)
        {
            notifs[index].SetActive(false); // Disable the notification
            if (pings.Length > index)
            {
                pings[index].SetActive(false); // Disable the respective ping
            }
        }
    }

    public void OnAppClicked(int appIndex)
    {
        if (appIndex >= 0 && appIndex < pings.Length)
        {
            pings[appIndex].SetActive(false);
        }
    }
}
