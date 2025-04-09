using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace AYO
{
    public class SpeakingArray : MonoBehaviour
    {
        [Header("Speaking �迭")]
        [SerializeField] private Speaking[] array;
        [SerializeField] private UnityEvent nextEvent;

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
