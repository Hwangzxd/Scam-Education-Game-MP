using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPopUp : MonoBehaviour
{
    public Transform notification;

    public void OnEnable()
    {
        notification.localPosition = new Vector2(0, Screen.height);
        notification.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }
}
