using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsHandler : MonoBehaviour
{
    RadioScript radioScript;
    public GameObject rotatingElement;

    public GameObject radioObject;
    public GameObject mapObject;
    public Animator mapAnim;
    public GameObject pageWithCodesObject;
    public Animator pageWithCodesAnim;
    public GameObject terminal;
    public Animator terminalAnim;
    public InputField terminalText;

    public GameObject hideAllButton;
    public GameObject buttonOnRadio;
    public GameObject buttonOffRadio;
    public GameObject LEDRadioOn;
    public GameObject LEDRadioOff;

    public GameObject buttonMultiplierHigher;
    public GameObject buttonMultiplierLower;
    public GameObject[] leverMultiplier;

    public GameObject buttonTransmitting;
    public GameObject buttonReceiving;
    public GameObject LEDTransmiting;
    public GameObject LEDNotTransmiting;

    public GameObject buttonCodingActive;
    public GameObject buttonCodingDeactive;

    public bool lightLED;
   

    void Start()
    {
        mapAnim = mapObject.GetComponent<Animator>();
        pageWithCodesAnim = pageWithCodesObject.GetComponent<Animator>();
        terminalAnim = terminal.GetComponent<Animator>();
        hideAllButton.SetActive(false);
        radioScript = radioObject.GetComponent<RadioScript>();
        terminalText.onValidateInput +=
         delegate (string s, int i, char c) { return char.ToUpper(c); };
    }

    void Update()
    {
        CheckThatOneLED();
    }


    public void CheckThatOneLED()
    {
        if (lightLED && radioScript.isRadioActive)
        {
            LEDTransmiting.SetActive(true);
            LEDNotTransmiting.SetActive(false);
        }
        else
        {
            LEDTransmiting.SetActive(false);
            LEDNotTransmiting.SetActive(true);
        }
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

    public void ShowTerminal()
    {
        terminalAnim.SetBool("showTerminal", true);
    }

    public void HideTerminal()
    {
        terminalAnim.SetBool("showTerminal", false);
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
            leverMultiplier[0].SetActive(false);
            leverMultiplier[1].SetActive(true);
            leverMultiplier[2].SetActive(false);
        }
        else
        {
            radioScript.frequencyMultipiler = 10f;
            leverMultiplier[0].SetActive(true);
            leverMultiplier[1].SetActive(false);
            leverMultiplier[2].SetActive(false);
        }
    }

    public void SetMultiplierToLower()
    {
        if (radioScript.frequencyMultipiler == 10f)
        {
            radioScript.frequencyMultipiler = 1f;
            leverMultiplier[0].SetActive(false);
            leverMultiplier[1].SetActive(true);
            leverMultiplier[2].SetActive(false);
        }
        else
        {
            radioScript.frequencyMultipiler = 0.1f;
            leverMultiplier[0].SetActive(false);
            leverMultiplier[1].SetActive(false);
            leverMultiplier[2].SetActive(true);
        }
    }

    public void SetToTransmitting()
    {
        buttonTransmitting.SetActive(false);
        buttonReceiving.SetActive(true);
        radioScript.isReceivingActive = false;
        lightLED = true;


    }

    public void SetToReceiving()
    {
        buttonTransmitting.SetActive(true);
        buttonReceiving.SetActive(false);
        radioScript.isReceivingActive = true;
        lightLED = false;
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
