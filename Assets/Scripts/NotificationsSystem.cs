using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class NotificationsSystem : MonoBehaviour
{
    public Notification[] notifications;
    public Clock clock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < notifications.Length; i++) 
        {
            if(clock.currentHours >= notifications[i].startTime && !notifications[i].wasShowed)
            {
                notifications[i].wasShowed = true;
            }
        }
    }
}
