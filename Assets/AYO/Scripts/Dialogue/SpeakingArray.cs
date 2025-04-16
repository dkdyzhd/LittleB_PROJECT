using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UltEvents;

namespace AYO
{
    public class SpeakingArray : MonoBehaviour
    {
        [Header("Speaking ¹è¿­")]
        [SerializeField] private Speaking[] array;
        [SerializeField] private UltEvent nextEvent;

        public Speaking GetSpeaking(int i)
        {
            return array[i];
        }

        public int GetArrayLength()
        {
            return array.Length;
        }

        public void InvokeNextEvent()
        {
            nextEvent.Invoke();
        }

        
    }
}
