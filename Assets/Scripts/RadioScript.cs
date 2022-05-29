using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioScript : MonoBehaviour
{
    public float frequency;
    public GameObject rotatingWheel;
    public GameObject Frame2;
    public bool isFrame2Active;

    public float shift;
    public float compression;

    public Slider compressionSlider;
    public Slider shiftSlider;

    public float frequencyMultipiler;
    public GameObject frequencyMeter;
    RectTransform frequencyMeterRectTransform;
    public float frequencyMeterPosition;

    public bool canChangeFrequency; //1 can change 0 
    public bool isCodingActive; //1 is active 0 is not active
    public bool isReceivingActive; // 1 is receiving 0 is transmiting
    public bool isRadioActive; //iks de de de 1 is On 0 is Off

    // Start is called before the first frame update
    void Start()
    {
        isFrame2Active = false;
        isRadioActive = false;
        frequency = 100;
        compression = 0.4f;
        compressionSlider.value = compression;
        shift = 0.5f;
        shiftSlider.value = shift;
        frequencyMultipiler = 1;
        frequencyMeterRectTransform = frequencyMeter.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeFrequency();
        ReadFromSliders();
    }

    void ChangeFrequency()
    {
        if(canChangeFrequency)
        {
            frequency += Input.mouseScrollDelta.y * frequencyMultipiler;
            Frame2.SetActive(isFrame2Active);
            if (Input.mouseScrollDelta.y != 0)
            {
                isFrame2Active = !isFrame2Active;
            }

            rotatingWheel.transform.Rotate(0, 0, Input.mouseScrollDelta.y * 5f);
            if (frequency < 50)
            {
                frequency = 50;
                rotatingWheel.transform.Rotate(0, 0, -Input.mouseScrollDelta.y * 5f);
                isFrame2Active = !isFrame2Active;
            }
            if (frequency > 310)
            {
                frequency = 310;
                rotatingWheel.transform.Rotate(0, 0, -Input.mouseScrollDelta.y * 5f);
                isFrame2Active = !isFrame2Active;
            }
        }
        frequencyMeterPosition = 38 + 1780 - 8 * frequency;
        //frequencyMeterRectTransform.anchoredPosition = new Vector2(frequencyMeterPosition, 120);
        frequencyMeterRectTransform.anchoredPosition = Vector2.MoveTowards(frequencyMeterRectTransform.anchoredPosition, new Vector2(frequencyMeterPosition, 88), 20f);
    }

    void ReadFromSliders()
    {
        compression = compressionSlider.value;
        shift = shiftSlider.value;
    }

}
