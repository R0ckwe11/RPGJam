using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    public float frequency;
    public bool canChangeFrequency;
    public GameObject rotatingWheel;
    public bool isCodingEnabled;
    public float shift;
    public float compression;
    public bool isRadioActive; //iks de de de


    // Start is called before the first frame update
    void Start()
    {
        frequency = 100;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeFrequency();
    }

    void ChangeFrequency()
    {
        if(canChangeFrequency)
        {
            frequency += Input.mouseScrollDelta.y / 2f;
            rotatingWheel.transform.Rotate(0, 0, Input.mouseScrollDelta.y * 5f);
            if (frequency < 90)
            {
                frequency = 90;
                rotatingWheel.transform.Rotate(0, 0, -Input.mouseScrollDelta.y * 5f);
            }
            if (frequency > 120)
            {
                frequency = 120;
                rotatingWheel.transform.Rotate(0, 0, -Input.mouseScrollDelta.y * 5f);
            }
        }
    }

}
