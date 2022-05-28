using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsHandler : MonoBehaviour
{
    RadioScript radioScript;
    public GameObject rotatingElement;
    public GameObject radioObject;
    public GameObject mapObject;
    public Animator mapAnim;
    public GameObject pageWithCodesObject;
    public Animator pageWithCodesAnim;
    public GameObject hideAllButton;
    void Start()
    {
        mapAnim = mapObject.GetComponent<Animator>();
        pageWithCodesAnim = pageWithCodesObject.GetComponent<Animator>();
        hideAllButton.SetActive(false);
        radioScript = radioObject.GetComponent<RadioScript>();
    }

    public void ShowMap()
    {
        mapAnim.SetBool("showMap", true);
        hideAllButton.SetActive(true);
    }

    public void ShowCodes()
    {
        pageWithCodesAnim.SetBool("showCodes", true);
        hideAllButton.SetActive(true);
    }

    public void HideAll()
    {
        pageWithCodesAnim.SetBool("showCodes", false);
        mapAnim.SetBool("showMap", false);
        hideAllButton.SetActive(false);
    }

    public void FrequencyChangeEnable()
    {
        radioScript.canChangeFrequency = true;
        radioScript.rotatingWheel = rotatingElement;
    }

    public void FrequencyChangeDissable()
    {
        radioScript.canChangeFrequency = false;
        radioScript.rotatingWheel = null;
    }
}
