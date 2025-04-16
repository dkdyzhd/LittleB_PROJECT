using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class SpeakingArrayList : MonoBehaviour
    {
        [SerializeField] private SpeakingArray[] speakingArrays;

        public SpeakingArray GetSpeakingArray(int i)
        {
            return speakingArrays[i];
        }
    }
}
