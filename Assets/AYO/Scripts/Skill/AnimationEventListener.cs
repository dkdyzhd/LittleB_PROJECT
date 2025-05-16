using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace AYO
{
    public class AnimationEventListener : MonoBehaviour
    {
        public UltEvent<string> onTakenAnimationEvent;

        public void OnAnimationEvent(string eventName)
        {
            onTakenAnimationEvent?.Invoke(eventName);
        }
    }
}
