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
    public GameObject LEDRadioOn;
    public GameObject LEDRadioOff;

    public GameObject buttonMultiplierHigher;
    public GameObject buttonMultiplierLower;

    public GameObject buttonTransmitting;
    public GameObject buttonReceiving;
    public GameObject LEDTransmiting;
    public GameObject LEDNotTransmiting;

    public GameObject buttonCodingActive;
    public GameObject buttonCodingDeactive;

   

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
        radioScript.isRadioActive = true;
        LEDRadioOn.SetActive(true);
        LEDRadioOff.SetActive(false);
    }

    public void OffRadio()
    {
        buttonOffRadio.SetActive(false);
        buttonOnRadio.SetActive(true);
        radioScript.isRadioActive = false;
        LEDRadioOn.SetActive(false);
        LEDRadioOff.SetActive(true);
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
        radioScript.isReceivingActive = false;
        LEDTransmiting.SetActive(true);
        LEDNotTransmiting.SetActive(false);
    }

    public void SetToReceiving()
    {
        buttonTransmitting.SetActive(true);
        buttonReceiving.SetActive(false);
        radioScript.isReceivingActive = true;
        LEDTransmiting.SetActive(false);
        LEDNotTransmiting.SetActive(true);
    }

    public void SetCodingToActive()
    {
        buttonCodingActive.SetActive(false);
        buttonCodingDeactive.SetActive(true);
        radioScript.isCodingActive = true;
    }

    public void SetCodingToNotActive()
    {
        buttonCodingActive.SetActive(true);
        buttonCodingDeactive.SetActive(false);
        radioScript.isCodingActive = false;
    }
}
