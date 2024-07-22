using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public GameObject[] notificationPrefabs; // Array of notification prefabs in the scene
    private List<int> shownNotifications = new List<int>();

    private void Start()
    {
        ShowRandomNotification();
    }

    private void ShowRandomNotification()
    {
        if (notificationPrefabs.Length == 0)
        {
            Debug.LogWarning("No notification prefabs assigned.");
            return;
        }

        List<int> availableNotifications = new List<int>();

        // Add all notifications that haven't been shown yet and are currently inactive
        for (int i = 0; i < notificationPrefabs.Length; i++)
        {
            if (!shownNotifications.Contains(i) && !notificationPrefabs[i].activeSelf)
            {
                availableNotifications.Add(i);
            }
        }

        // If all notifications have been shown, reset the list
        if (availableNotifications.Count == 0)
        {
            shownNotifications.Clear();
            for (int i = 0; i < notificationPrefabs.Length; i++)
            {
                if (!notificationPrefabs[i].activeSelf)
                {
                    availableNotifications.Add(i);
                }
            }
        }

        // If still no available notifications, return
        if (availableNotifications.Count == 0)
        {
            Debug.LogWarning("No available notifications to show.");
            return;
        }

        // Select a random notification from the available list
        int randomIndex = availableNotifications[Random.Range(0, availableNotifications.Count)];
        shownNotifications.Add(randomIndex);

        // Enable the selected notification
        notificationPrefabs[randomIndex].SetActive(true);
        Debug.Log("Notification activated: " + notificationPrefabs[randomIndex].name);
    }
}
