using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class MobileNotification : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // remove mesages
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        //create an android notification chanel
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Needs Channel",
            Importance = Importance.Default,
            Description = "Reminder  notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        //create the notification  that is going to be sent
        var notification = new AndroidNotification();
        notification.Title = "Hey! I'm Hungry";
        notification.Text = "I'm your pet, feed me <3";
        notification.FireTime = System.DateTime.Now.AddSeconds(15);

        //send the notification 
        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        //if the scrip is run a message is already scheduel
        if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id)==NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

    // Update is called once per frame  
    void Update()
    {
        
    }
}
