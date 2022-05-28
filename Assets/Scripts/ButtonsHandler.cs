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
    public GameObject buttonOnRadio;
    public GameObject buttonOffRadio;

    public GameObject buttonMultiplierHigher;
    public GameObject buttonMultiplierLower;

    public GameObject buttonTransmitting;
    public GameObject buttonReceiving;
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
    }

    public void FrequencyChangeDissable()
    {
        radioScript.canChangeFrequency = false;
    }

    public void OnRadio()
    {
        buttonOffRadio.SetActive(true);
        buttonOnRadio.SetActive(false);
    }

    public void OffRadio()
    {
        buttonOffRadio.SetActive(false);
        buttonOnRadio.SetActive(true);
    }

    public void SetMultiplierToHigher()
    {
        if (radioScript.frequencyMultipiler == 0.1f)
        {
            radioScript.frequencyMultipiler = 1f;
        }
        else
        {
            radioScript.frequencyMultipiler = 10f;
        }
    }

    public void SetMultiplierToLower()
    {
        if (radioScript.frequencyMultipiler == 10f)
        {
            radioScript.frequencyMultipiler = 1f;
        }
        else
        {
            radioScript.frequencyMultipiler = 0.1f;
        }
    }

    public void SetToTransmitting()
    {
        buttonTransmitting.SetActive(false);
        buttonReceiving.SetActive(true);
    }

    public void SetToReceiving()
    {
        buttonTransmitting.SetActive(true);
        buttonReceiving.SetActive(false);
    }
}
