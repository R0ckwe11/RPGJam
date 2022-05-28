using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Clock : MonoBehaviour
    {
        public MainGameLogic gameLogic;
        public int currentHours = 7;
        public float currentMinutes = 0;
        public bool timerIsRunning = true;
        public float dilation = 1;
        public Text timeHours;
        public Text timeMinutes;
        public Text timeColon;

        private float colonTimer = 0;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (timerIsRunning)
            {
                currentMinutes += (Time.deltaTime * dilation);
                if (currentMinutes >= 60)
                {
                    currentMinutes -= 60;
                    currentHours += 1;
                    if (currentHours >= 24)
                    {
                        currentHours = 0;
                        // gameLogic.NextDay();
                    }
                }
                DisplayTime(GetTime());
            }

            colonTimer += Time.deltaTime;
            if (colonTimer >= 1)
            {
                timeColon.enabled = !timeColon.enabled;
                colonTimer -= 1;
            }
        }

        float[] GetTime()
        {
            return new[] { currentHours, currentMinutes };
        }

        void DisplayTime(float[] timeToDisplay)
        {
            int hours = (int)timeToDisplay[0];
            int minutes = Mathf.FloorToInt(timeToDisplay[1]);
            timeHours.text = $"{hours}";
            timeMinutes.text = $"{minutes:00}";
        }

        void ChangeDilation(float newDilation)
        {
            dilation = newDilation;
        }
    }
}
