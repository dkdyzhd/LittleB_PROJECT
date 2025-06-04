using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class InteractionPainting : MonoBehaviour
    {
        [SerializeField] GameObject onPainting;
        [SerializeField] ScreenFader screenFader;
        private float fadeTimer = 0.5f;
        private bool on = false;

        private void Update()
        {
            
        }
        public void OnPainting()
        {
            onPainting.SetActive(true);
            
            screenFader.FadeOutToIn();
        }

    }
}
