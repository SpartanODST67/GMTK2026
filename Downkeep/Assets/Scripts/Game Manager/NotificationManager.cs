using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }
    [SerializeField] Notifications uiComponent;
    LinkedList<string> notifications = new();
    [SerializeField] int maxNotifications = 5;

    void Awake()
    {
        Instance = this;
    }

    public void Notification(string msg)
    {
        if(notifications.Count > maxNotifications)
        {
            notifications.RemoveFirst();
        }

        notifications.AddLast(msg);

        uiComponent.WriteNotifications(notifications);
    }

}
