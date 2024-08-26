using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotificationManager : MonoBehaviour
{
    public YabberData yabberData;
    public Notification[] notifications; // Array of Notification objects
    public GameObject INVS;

    private List<int> shownNotifs = new List<int>();

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ResetBlockedApps(); // Reset blocked apps when the scene starts
        UpdateNotifs();
    }

    void Update()
    {
        // Check if the INVS notification is active in the scene
        if (INVS != null && INVS.activeInHierarchy)
        {
            yabberData.isINVSClicked = true;
        }
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
            StartCoroutine(ShowRandomNotifWithDelay()); // Add delay before showing notification
        }
        else
        {
            foreach (var notif in notifications)
            {
                if (notif.notification != null)
                {
                    notif.notification.SetActive(false);
                }

                if (notif.ping != null)
                {
                    notif.ping.SetActive(false);
                }

                if (notif.blocker != null)
                {
                    notif.blocker.SetActive(false); // Ensure blockers are hidden
                }
            }
        }
    }

    private IEnumerator ShowRandomNotifWithDelay()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before showing the notification
        ShowRandomNotif();
    }

    private void ShowRandomNotif()
    {
        if (notifications.Length == 0)
        {
            Debug.LogWarning("No notifications assigned.");
            return;
        }

        List<int> availableNotifs = new List<int>();

        for (int i = 0; i < notifications.Length; i++)
        {
            // Check if the notif is inactive & not blocked by MGData
            if (!shownNotifs.Contains(i) && !notifications[i].notification.activeSelf && !IsNotificationBlocked(i))
            {
                availableNotifs.Add(i);
            }
        }

        // If all notifications have been shown, reset the list
        if (availableNotifs.Count == 0)
        {
            shownNotifs.Clear();
            for (int i = 0; i < notifications.Length; i++)
            {
                if (!notifications[i].notification.activeSelf && !IsNotificationBlocked(i))
                {
                    availableNotifs.Add(i);
                }
            }
        }

        if (availableNotifs.Count == 0)
        {
            Debug.LogWarning("No available notifications to show.");
            return;
        }

        int randomIndex = availableNotifs[Random.Range(0, availableNotifs.Count)];
        shownNotifs.Add(randomIndex);

        if (notifications[randomIndex].notification != null)
        {
            notifications[randomIndex].notification.SetActive(true);
        }

        BlockAppsExcept(randomIndex); // Block all apps except the chosen one

        if (notifications[randomIndex].ping != null)
        {
            notifications[randomIndex].ping.SetActive(true);
        }

        if (notifications[randomIndex].blocker != null)
        {
            notifications[randomIndex].blocker.SetActive(false); // Disable the blocker for the randomly chosen notification
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
        for (int i = 0; i < notifications.Length; i++)
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
        if (index >= 0 && index < notifications.Length)
        {
            if (notifications[index].notification != null)
            {
                notifications[index].notification.SetActive(false); // Disable the notification
            }
            if (notifications[index].ping != null)
            {
                notifications[index].ping.SetActive(false); // Disable the respective ping
            }
            if (notifications[index].blocker != null)
            {
                notifications[index].blocker.SetActive(true); // Activate blocker for the notification
            }
        }
    }

    private void BlockAppsExcept(int indexToKeep)
    {
        for (int i = 0; i < notifications.Length; i++)
        {
            if (i != indexToKeep)
            {
                if (notifications[i].ping != null)
                {
                    notifications[i].ping.SetActive(false); // Block the app by deactivating its ping
                }
                if (notifications[i].blocker != null)
                {
                    notifications[i].blocker.SetActive(true); // Activate blocker for other apps
                }
            }
        }
    }

    private void ResetBlockedApps()
    {
        // Reset all blockers except for the chosen one
        for (int i = 0; i < notifications.Length; i++)
        {
            if (notifications[i].blocker != null)
            {
                notifications[i].blocker.SetActive(true);
            }
        }
    }

    public void OnAppClicked(int appIndex)
    {
        if (appIndex >= 0 && appIndex < notifications.Length)
        {
            if (notifications[appIndex].ping != null)
            {
                notifications[appIndex].ping.SetActive(false); // Deactivate the ping when the app is clicked
            }
        }
    }

    public void OnINVSNotificationClicked()
    {
        if (yabberData == null)
        {
            Debug.LogError("YabberData is not assigned");
            return;
        }

        yabberData.isINVSClicked = true; // Update the GOISClicked boolean
        Debug.Log("INVS notification clicked.");
    }

    public void OnGOISNotificationClicked()
    {
        if (yabberData == null)
        {
            Debug.LogError("YabberData is not assigned");
            return;
        }

        yabberData.isGOISClicked = true; // Update the GOISClicked boolean
        Debug.Log("GOIS notification clicked.");
    }
}
