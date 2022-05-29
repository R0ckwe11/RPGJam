using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    //public string text;
    public Text textObject;
    public Animator anim;
    public GameObject clockGameObject;
    public Clock clock;
    public GameObject Terminal2;
    public float timer;
    public float startTime;
    public float startDay;
    public bool wasShowed;

    void Start()
    {
        anim = Terminal2.GetComponent<Animator>();
        timer = 0;
    }

    public void SetNotification(string text, float time)
    {
        anim.SetBool("showTerminal2", true);
        textObject.text = text;
        timer = time;
    }

    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("showTerminal2", false);
        }
    }
}
