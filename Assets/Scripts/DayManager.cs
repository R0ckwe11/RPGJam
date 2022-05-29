using System.Collections.Generic;
using System.Linq;
using Assets.Classes;
using UnityEngine;

namespace Assets.Scripts
{
    public class DayManager : MonoBehaviour
    {
        public Day activeDay;

        private readonly List<Day> days = new();
        // Start is called before the first frame update
        void Start()
        {
            LoadDays();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void LoadDays()
        {
            days.Clear();
            // day 1
            days.Add(new Day1());
            // day 2
            // day 3
        }

        public void SetDay(int day)
        {
            activeDay = days.FirstOrDefault(d => d.dayID == day);
        }
    }
}
