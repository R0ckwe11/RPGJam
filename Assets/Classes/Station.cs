using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Rendering;

namespace Assets.Classes
{
    public class Station
    {
        public int ID;
        public string info;
        public float letterTimer = 1.0f;
        public bool letterPlaying = false;
        public Queue<string> letterQueue;
    }
}
