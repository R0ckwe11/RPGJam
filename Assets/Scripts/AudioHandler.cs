using System;
using System.Collections.Generic;
using Assets.Classes;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioHandler : MonoBehaviour
    {
        public RadioScript radio;
        // public GameLogic gameLogic;
        public DayManager dayManager;
        public AudioSource noiseDefault;
        public AudioSource noiseCoding;
        public AudioSource noiseFound;
        public AudioSource noiseTransmit;
        // 0 - none
        // 1 - default
        // 2 - transmit
        // 3 - found
        // 4 - coding
        public int activeNoise;
        public AudioSource intelLetters;
        public AudioSource squadLetters;
        public AudioSource numeric1Letters;
        public AudioSource numeric2Letters;
        public AudioSource numeric3Letters;
        // 0 - none
        // 1,2,3 - numeric
        // 4 - intel
        // 5 - squad
        public int activeLetters;
        public AudioDistortionFilter transmitDistortion;
        public AudioHighPassFilter transmitHighPass;
        public AudioClip[] letters;
        public Station[] stations;

        // Start is called before the first frame update
        void Start()
        {
            var newStations = new List<Station>();
            for (var i = 1; i < 6; i++)
            {
                newStations.Add(new Station
                {
                    ID = i,
                    letterPlaying = false,
                    letterTimer = 1.0f,
                    letterQueue = new Queue<string>()
                });
            }
            stations = newStations.ToArray();

            dayManager.LoadDays();
            dayManager.SetDay(1);

            foreach (var station in stations)
            {
                station.info = station.ID switch
                {
                    1 => dayManager.activeDay.numericInfo1,
                    2 => dayManager.activeDay.numericInfo2,
                    3 => dayManager.activeDay.numericInfo3,
                    4 => dayManager.activeDay.intelInfo,
                    5 => dayManager.activeDay.squadInfo,
                    _ => station.info
                };
            }
            transmitDistortion.distortionLevel = 0.8f;
            transmitHighPass.cutoffFrequency = 1500;
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var station in stations)
            {
                if (station.letterPlaying)
                {
                    station.letterTimer -= Time.deltaTime;
                    if (station.letterTimer <= 0)
                    {
                        station.letterPlaying = false;
                        station.letterTimer = 1.0f;
                    }
                }

                if (station.letterQueue.Count != 0 && !station.letterPlaying)
                {
                    var letter = station.letterQueue.Dequeue();

                    if (GetCurrentLetterSource())
                    {
                        PlayLetter(letter, station.ID - 1);
                    }
                }

                if (station.letterQueue.Count == 0 && !station.letterPlaying)
                {
                    LoadInfoToQueue(station.ID - 1);
                }
            }
        }

        public void UpdateNoise()
        {
            if (!radio.isRadioActive)
            {
                noiseDefault.mute = true;
                noiseCoding.mute = true;
                noiseTransmit.mute = true;
                transmitDistortion.enabled = false;
                transmitHighPass.enabled = false;
                activeNoise = 0;
                return;
            }
            
            if (radio.isTransmittingActive && radio.isCodingActive)
            {
                noiseDefault.mute = true;
                noiseCoding.mute = true;
                noiseTransmit.mute = false;
                transmitDistortion.enabled = true;
                transmitHighPass.enabled = true;
                activeNoise = 2;
                return;
            }
            
            if (radio.isCodingActive)
            {
                noiseDefault.mute = true;
                noiseCoding.mute = false;
                noiseTransmit.mute = true;
                transmitDistortion.enabled = false;
                transmitHighPass.enabled = false;
                activeNoise = 4;
                return;
            }
            
            if (radio.isTransmittingActive)
            {
                noiseDefault.mute = true;
                noiseCoding.mute = true;
                noiseTransmit.mute = false;
                transmitDistortion.enabled = false;
                transmitHighPass.enabled = false;
                activeNoise = 2;
                return;
            }

            noiseDefault.mute = false;
            noiseCoding.mute = true;
            noiseTransmit.mute = true;
            transmitDistortion.enabled = false;
            transmitHighPass.enabled = false;
            activeNoise = 1;
        }

        public void UpdateLetters()
        {
            var tolerance = dayManager.activeDay.tolerance;
            // intel
            if (radio.frequency >= dayManager.activeDay.intel - tolerance &&
                radio.frequency <= dayManager.activeDay.intel + tolerance)
            {
                activeLetters = 4;
                intelLetters.mute = false;
                return;
            }
            intelLetters.mute = true;
            
            // squad
            if (radio.frequency >= dayManager.activeDay.squad - tolerance &&
                radio.frequency <= dayManager.activeDay.squad + tolerance)
            {
                activeLetters = 5;
                squadLetters.mute = false;
                return;
            }
            squadLetters.mute = true;
            
            // numeric1
            if (radio.frequency >= dayManager.activeDay.numeric1 - tolerance &&
                radio.frequency <= dayManager.activeDay.numeric1 + tolerance)
            {
                activeLetters = 1;
                numeric1Letters.mute = false;
                return;
            }
            numeric1Letters.mute = true;
            
            // numeric2
            if (radio.frequency >= dayManager.activeDay.numeric2 - tolerance &&
                radio.frequency <= dayManager.activeDay.numeric2 + tolerance)
            {
                activeLetters = 2;
                numeric2Letters.mute = false;
                return;
            }
            numeric2Letters.mute = true;
            
            // numeric3
            if (radio.frequency >= dayManager.activeDay.numeric3 - tolerance &&
                radio.frequency <= dayManager.activeDay.numeric3 + tolerance)
            {
                activeLetters = 3;
                numeric3Letters.mute = false;
                return;
            }
            numeric3Letters.mute = true;

            activeLetters = 0;
        }

        public void BlendSources()
        {
            var tolerance = dayManager.activeDay.tolerance;
            float difference = 1;
            // intel
            if (radio.frequency >= dayManager.activeDay.intel - tolerance &&
                radio.frequency <= dayManager.activeDay.intel + tolerance)
            {
                difference = GetCurrentNoiseSource().volume =
                    MathF.Abs((radio.frequency - dayManager.activeDay.intel) / tolerance);
            }
            // squad
            if (radio.frequency >= dayManager.activeDay.squad - tolerance &&
                radio.frequency <= dayManager.activeDay.squad + tolerance)
            {
                difference = GetCurrentNoiseSource().volume =
                    MathF.Abs((radio.frequency - dayManager.activeDay.squad) / tolerance);
            }
            // numeric1
            if (radio.frequency >= dayManager.activeDay.numeric1 - tolerance &&
                radio.frequency <= dayManager.activeDay.numeric1 + tolerance)
            {
                difference = GetCurrentNoiseSource().volume =
                    MathF.Abs((radio.frequency - dayManager.activeDay.numeric1) / tolerance);
            }
            // numeric2
            if (radio.frequency >= dayManager.activeDay.numeric2 - tolerance &&
                radio.frequency <= dayManager.activeDay.numeric2 + tolerance)
            {
                difference = GetCurrentNoiseSource().volume =
                    MathF.Abs((radio.frequency - dayManager.activeDay.numeric2) / tolerance);
            }
            // numeric3
            if (radio.frequency >= dayManager.activeDay.numeric3 - tolerance &&
                radio.frequency <= dayManager.activeDay.numeric3 + tolerance)
            {
                difference = GetCurrentNoiseSource().volume =
                    MathF.Abs((radio.frequency - dayManager.activeDay.numeric3) / tolerance);
            }

            if (GetCurrentLetterSource())
            {
                GetCurrentLetterSource().volume = 1.0f - difference;
            }
        }

        public void PlayLetter(string letter, int stationID)
        {
            if (stations[stationID].letterPlaying)
            {
                stations[stationID].letterQueue.Enqueue(letter);
                return;
            }

            if (letter == "beep")
            {
                stations[stationID].letterTimer = 6.0f;
            }

            foreach(var clip in letters){
                if (clip.name == letter)
                {
                    // Debug.Log($"{stationID} : {letter}");
                    if (GetLetterSource(stationID + 1))
                    {
                        GetLetterSource(stationID + 1).PlayOneShot(clip);
                    }
                    stations[stationID].letterPlaying = true;
                }
            }
        }

        [CanBeNull]
        public AudioSource GetCurrentNoiseSource()
        {
            return activeNoise switch
            {
                0 => null,
                1 => noiseDefault,
                2 => noiseTransmit,
                3 => noiseFound,
                4 => noiseCoding,
                _ => null
            };
        }

        [CanBeNull]
        public AudioSource GetCurrentLetterSource()
        {
            return activeLetters switch
            {
                0 => null,
                1 => numeric1Letters,
                2 => numeric2Letters,
                3 => numeric3Letters,
                4 => intelLetters,
                5 => squadLetters,
                _ => null
            };
        }

        public AudioSource GetLetterSource(int stationID)
        {
            return stationID switch
            {
                0 => null,
                1 => numeric1Letters,
                2 => numeric2Letters,
                3 => numeric3Letters,
                4 => intelLetters,
                5 => squadLetters,
                _ => null
            };
        }

        public void ClearQueues()
        {
            foreach (var station in stations)
            {
                station.letterQueue.Clear();
            }
        }

        public void LoadInfoToQueue(int stationID)
        {
            var letters = stations[stationID].info.ToCharArray();
            foreach (var c in letters)
            {
                stations[stationID].letterQueue.Enqueue(c.ToString());
            }
            stations[stationID].letterQueue.Enqueue("beep");
        }
    }
}
