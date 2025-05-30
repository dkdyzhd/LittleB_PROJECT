using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class ScreenFader : MonoBehaviour
    {
        public Animator animator { get; private set; }
        private void Awake()
        {
            if (animator == null) animator = GetComponent<Animator>();
        }
        public void ScreenFadeIn()
        {
            animator.Play("FadeIn");
        }

        public void ScreenFadeOut()
        {
            animator.Play("FadeOut");
        }        
    }
}
