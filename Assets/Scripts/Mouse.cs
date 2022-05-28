using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    RadioScript radioScript;
    public GameObject radioObject;
    public GameObject lightobject;
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
        lightobject.transform.position = collision.gameObject.transform.position;
        if (collision.gameObject.transform.CompareTag("FrequencyChange")) 
        {
            radioScript.canChangeFrequency = true;
            radioScript.rotatingWheel = collision.gameObject;
        }
        if(collision.gameObject.transform.CompareTag("UI"))
        {
            Debug.Log("Znalaz³em canvas");
            lightobject.transform.position = collision.gameObject.transform.position;
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
