using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotificationManager : MonoBehaviour
{
    public YabberData yabberData;
    public Notification[] notifications; // Array of Notification objects
    public GameObject INVS;

    private bool isNotificationActive = false;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ResetBlockedApps(); // Reset blocked apps when the scene starts
        UpdateNotifs();
    }

    void Update()
    {

        if (INVS != null && INVS.activeInHierarchy)
        {
            yabberData.isINVSClicked = true;
            Debug.Log("INVS is active and isINVSClicked set to true."); // Debug log to confirm the condition is met
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

        NotifData data = NotifData.Instance;

        if (data == null)
        {
            Debug.LogError("NotifData instance is missing.");
            return;
        }

        if (currentScene == "Home")
        {
            if (!isNotificationActive)
            {
                StartCoroutine(ShowRandomNotifWithDelay()); // Add delay before showing notification
            }
        }
        else
        {
            HideAllNotifs(); // Hide all notifications in non-Home scenes
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

        List<int> availableNotifs = GetAvailableNotifs();

        if (availableNotifs.Count == 0)
        {
            NotifData.Instance.ClearShownNotifs();
            availableNotifs = GetAvailableNotifs();
        }

        if (availableNotifs.Count == 0)
        {
            Debug.LogWarning("No available notifications to show.");
            return;
        }

        int randomIndex = availableNotifs[Random.Range(0, availableNotifs.Count)];
        NotifData.Instance.RegisterShownNotif(randomIndex);

        Debug.Log($"Showing notification {randomIndex}.");
        ActivateNotification(randomIndex);
    }

    private List<int> GetAvailableNotifs()
    {
        List<int> availableNotifs = new List<int>();

        for (int i = 0; i < notifications.Length; i++)
        {
            // Check if the notif is inactive & not blocked by NotifData
            if (!NotifData.Instance.shownNotifs.Contains(i) && !notifications[i].notification.activeSelf)
            {
                availableNotifs.Add(i);
            }
        }

        return availableNotifs;
    }

    private void ActivateNotification(int index)
    {
        if (notifications[index].notification != null)
        {
            notifications[index].notification.SetActive(true);
            isNotificationActive = true; // Indicate that a notification is currently active
        }

        BlockAppsExcept(index); // Block all apps except the chosen one

        if (notifications[index].ping != null)
        {
            notifications[index].ping.SetActive(true);
        }

        if (notifications[index].blocker != null)
        {
            notifications[index].blocker.SetActive(false); // Disable the blocker for the randomly chosen notification
        }

        StartCoroutine(DeactivateNotificationAfterDelay(index)); // Ensure the notification is deactivated after a period
    }

    private IEnumerator DeactivateNotificationAfterDelay(int index)
    {
        yield return new WaitForSeconds(10f); // Adjust the delay as needed
        if (notifications[index].notification != null)
        {
            notifications[index].notification.SetActive(false);
        }

        isNotificationActive = false; // Indicate that no notification is currently active
        UpdateNotifs(); // Check for the next notification
    }

    private void HideAllNotifs()
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
                notif.blocker.SetActive(false);
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
        // Reset all blockers
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
