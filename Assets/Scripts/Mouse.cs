using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    RadioScript radioScript;
    public GameObject radioObject;
    public GameObject outOfCamera;

    // Start is called before the first frame update
    void Start()
    {
        radioScript = radioObject.GetComponent<RadioScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Input.mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("FrequencyChange")) 
        {
            radioScript.canChangeFrequency = true;
            radioScript.rotatingWheel = collision.gameObject;
        }
        if(collision.gameObject.transform.CompareTag("UI"))
        {
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //lightobject.transform.position = outOfCamera.gameObject.transform.position;
        if (collision.gameObject.transform.CompareTag("FrequencyChange"))
        {
            radioScript.canChangeFrequency = false;
            radioScript.rotatingWheel = null;
        }
    }

}
