using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioHandler : MonoBehaviour
    {
        public RadioScript radio;
        public GameLogic gameLogic;
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
        public AudioSource station;
        public AudioDistortionFilter transmitDistortion;
        public AudioHighPassFilter TransmitHighPass;
        public AudioClip[] letters;
        public float letterTimer = 1.0f;
        public bool letterPlaying = false;
        public Queue<string> letterQueue;

        // Start is called before the first frame update
        void Start()
        {
            letterTimer = 1.0f;
            transmitDistortion.distortionLevel = 0.8f;
            TransmitHighPass.cutoffFrequency = 1500;
        }

        // Update is called once per frame
        void Update()
        {
            if (letterPlaying)
            {
                letterTimer -= Time.deltaTime;
                if (letterTimer <= 0)
                {
                    letterPlaying = false;
                    letterTimer = 1.0f;
                }
            }

            if (letterQueue != null && !letterPlaying)
            {
                var letter = letterQueue.Dequeue();
                PlayLetter(letter);
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
                TransmitHighPass.enabled = false;
                activeNoise = 0;
                return;
            }
            
            if (radio.isTransmittingActive && radio.isCodingActive)
            {
                noiseDefault.mute = true;
                noiseCoding.mute = true;
                noiseTransmit.mute = false;
                transmitDistortion.enabled = true;
                TransmitHighPass.enabled = true;
                activeNoise = 2;
                return;
            }
            
            if (radio.isCodingActive)
            {
                noiseDefault.mute = true;
                noiseCoding.mute = false;
                noiseTransmit.mute = true;
                transmitDistortion.enabled = false;
                TransmitHighPass.enabled = false;
                activeNoise = 4;
                return;
            }
            
            if (radio.isTransmittingActive)
            {
                noiseDefault.mute = true;
                noiseCoding.mute = true;
                noiseTransmit.mute = false;
                transmitDistortion.enabled = false;
                TransmitHighPass.enabled = false;
                activeNoise = 2;
                return;
            }

            noiseDefault.mute = false;
            noiseCoding.mute = true;
            noiseTransmit.mute = true;
            transmitDistortion.enabled = false;
            TransmitHighPass.enabled = false;
            activeNoise = 1;
        }

        public void FadeNoise()
        {
            var tolerance = gameLogic.dayManager.activeDay.tolerance;
            // intel
            if (radio.frequency >= gameLogic.dayManager.activeDay.intel - tolerance &&
                radio.frequency <= gameLogic.dayManager.activeDay.intel + tolerance)
            {
                GetCurrentAudioSource().volume =
                    MathF.Abs((radio.frequency - gameLogic.dayManager.activeDay.intel) / tolerance);
            }
            // squad
            if (radio.frequency >= gameLogic.dayManager.activeDay.squad - tolerance &&
                radio.frequency <= gameLogic.dayManager.activeDay.squad + tolerance)
            {
                GetCurrentAudioSource().volume =
                    MathF.Abs((radio.frequency - gameLogic.dayManager.activeDay.squad) / tolerance);
            }
            // numeric1
            if (radio.frequency >= gameLogic.dayManager.activeDay.numeric1 - tolerance &&
                radio.frequency <= gameLogic.dayManager.activeDay.numeric1 + tolerance)
            {
                GetCurrentAudioSource().volume =
                    MathF.Abs((radio.frequency - gameLogic.dayManager.activeDay.numeric1) / tolerance);
            }
            // numeric2
            if (radio.frequency >= gameLogic.dayManager.activeDay.numeric2 - tolerance &&
                radio.frequency <= gameLogic.dayManager.activeDay.numeric2 + tolerance)
            {
                GetCurrentAudioSource().volume =
                    MathF.Abs((radio.frequency - gameLogic.dayManager.activeDay.numeric2) / tolerance);
            }
            // numeric3
            if (radio.frequency >= gameLogic.dayManager.activeDay.numeric3 - tolerance &&
                radio.frequency <= gameLogic.dayManager.activeDay.numeric3 + tolerance)
            {
                GetCurrentAudioSource().volume =
                    MathF.Abs((radio.frequency - gameLogic.dayManager.activeDay.numeric3) / tolerance);
            }
        }

        public void PlayLetter(string letter)
        {
            if (letterPlaying)
            {
                letterQueue.Enqueue(letter);
                return;
            }

            foreach(var clip in letters){
                if (clip.name == letter)
                {
                    station.PlayOneShot(clip);
                    letterPlaying = true;
                }
            }
        }

        [CanBeNull]
        public AudioSource GetCurrentAudioSource()
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

        public void ClearQueue()
        {
            letterQueue.Clear();
        }
    }
}
