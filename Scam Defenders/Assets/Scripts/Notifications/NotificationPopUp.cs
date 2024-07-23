using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPopUp : MonoBehaviour
{
    public Transform notification;

    public void OnEnable()
    {
        Debug.Log("Notification OnEnable called: " + gameObject.name);
        notification.localPosition = new Vector2(0, Screen.height);
        notification.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;

        // Start coroutine to close the notification after 5 seconds
        StartCoroutine(CloseAfterDelay(5f));
    }

    public void Close()
    {
        Debug.Log("Closing notification: " + gameObject.name);
        notification.LeanMoveLocalY(Screen.height, 0.5f).setEaseInExpo().delay = 0.1f;
    }

    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Close();
    }
}
