using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotificationManager : MonoBehaviour
{
    public YabberData yabberData;
    public Notification[] youngNotifications; // Array for young notifications
    public Notification[] oldNotifications;   // Array for old notifications
    public Notification[] allNotifications;   // Array for all notifications
    public GameObject INVS;
    public GameObject scamCall;

    private bool isNotificationActive = false;
    private List<Notification> currentNotifications; // Array to use based on age
    private bool allNotificationsUsed = false; // Flag to check if all notifications have been used

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ResetBlockedApps(); // Reset blocked apps when the scene starts
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
        if (NotifData.Instance == null)
        {
            Debug.LogError("NotifData instance is missing.");
            return;
        }

        // Get the player's age index from SelectionManager
        SelectionManager selectionManager = FindObjectOfType<SelectionManager>();
        if (selectionManager == null)
        {
            Debug.LogError("SelectionManager instance is missing.");
            return;
        }

        int playerScenario = selectionManager.GetPlayerScenario();

        // Select notifications based on age
        currentNotifications = playerScenario == 1 ? new List<Notification>(youngNotifications) : new List<Notification>(oldNotifications);
        allNotificationsUsed = false; // Reset the flag

        if (currentScene == "Home")
        {
            if (!isNotificationActive && !IsScamCallActive())
            {
                StartCoroutine(ShowRandomNotifWithDelay());
            }
        }
        else
        {
            HideAllNotifs();
        }
    }

    private bool IsScamCallActive()
    {
        if (scamCall == null)
        {
            Debug.LogWarning("ScamCall GameObject is not assigned or is missing.");
            return false;
        }

        bool isActive = scamCall.activeInHierarchy;
        Debug.Log("ScamCall GameObject is " + (isActive ? "active" : "inactive") + " in the scene.");

        return isActive;
    }

    private IEnumerator ShowRandomNotifWithDelay()
    {
        yield return new WaitForSeconds(2f);
        ShowRandomNotif();
    }

    private void ShowRandomNotif()
    {
        // Check if we need to switch to all notifications
        if (currentNotifications.Count == 0 || allNotificationsUsed)
        {
            Debug.Log("Switching to all notifications.");
            currentNotifications = new List<Notification>(allNotifications);
            allNotificationsUsed = true; // Mark that we've started showing all notifications
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

        for (int i = 0; i < currentNotifications.Count; i++)
        {
            if (!NotifData.Instance.shownNotifs.Contains(i) && !currentNotifications[i].notification.activeSelf)
            {
                availableNotifs.Add(i);
            }
        }

        return availableNotifs;
    }

    private void ActivateNotification(int index)
    {
        if (currentNotifications[index].notification != null)
        {
            currentNotifications[index].notification.SetActive(true);
            isNotificationActive = true;

            // Check if the INVS notification is being activated
            if (currentNotifications[index].notification == INVS)
            {
                yabberData.isINVSClicked = true;
                Debug.Log("INVS notification appeared, setting isINVSClicked to true.");
            }

            // Check if the GOIS notification is being activated
            if (currentNotifications[index].notification == scamCall)
            {
                yabberData.isGOISClicked = true;
                Debug.Log("GOIS notification appeared, setting isGOISClicked to true.");
            }
        }

        BlockAppsExcept(index);

        if (currentNotifications[index].ping != null)
        {
            currentNotifications[index].ping.SetActive(true);
        }

        if (currentNotifications[index].blocker != null)
        {
            currentNotifications[index].blocker.SetActive(false);
        }

        StartCoroutine(DeactivateNotificationAfterDelay(index));
    }


    private IEnumerator DeactivateNotificationAfterDelay(int index)
    {
        if (currentNotifications[index].notification == scamCall)
        {
            Debug.Log("Scam call notification detected. Skipping deactivation delay.");
            yield break;
        }

        yield return new WaitForSeconds(10f);

        if (currentNotifications[index].notification != null)
        {
            currentNotifications[index].notification.SetActive(false);
        }

        isNotificationActive = false;
        UpdateNotifs();
    }

    private void HideAllNotifs()
    {
        foreach (var notif in currentNotifications)
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
        for (int i = 0; i < currentNotifications.Count; i++)
        {
            if (i != indexToKeep)
            {
                if (currentNotifications[i].ping != null)
                {
                    currentNotifications[i].ping.SetActive(false);
                }
                if (currentNotifications[i].blocker != null)
                {
                    currentNotifications[i].blocker.SetActive(true);
                }
            }
        }
    }

    private void ResetBlockedApps()
    {
        for (int i = 0; i < allNotifications.Length; i++)
        {
            if (allNotifications[i].blocker != null)
            {
                allNotifications[i].blocker.SetActive(true);
            }
        }
    }

    public void OnAppClicked(int appIndex)
    {
        if (appIndex >= 0 && appIndex < currentNotifications.Count)
        {
            if (currentNotifications[appIndex].ping != null)
            {
                currentNotifications[appIndex].ping.SetActive(false);
            }
        }
    }
}
