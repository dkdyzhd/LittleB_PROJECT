using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class SpeakingArray : MonoBehaviour
    {
        [Header("Speaking �迭")]
        [SerializeField] private Speaking[] array;

        public Speaking GetSpeaking(int i)
        {
            return array[i];
        }

    }
}
