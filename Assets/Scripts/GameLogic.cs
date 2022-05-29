using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameLogic : MonoBehaviour
    {
        public DayManager dayManager;
        public RadioScript radio;
        public AudioHandler audioHandler;
        public int currentDay;

        // Start is called before the first frame update
        void Start()
        {
            currentDay = 1;
            dayManager.SetDay(currentDay);
        }

        // Update is called once per frame
        void Update()
        {
            audioHandler.UpdateNoise();
            audioHandler.UpdateLetters();
            if (audioHandler.GetCurrentNoiseSource())
            {
                audioHandler.BlendSources();
            }
        }

        public void NextDay()
        {
            var currentDayID = dayManager.activeDay.dayID;
            dayManager.SetDay(currentDayID + 1);
            currentDay += 1;
        }
    }
}
