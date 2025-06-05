using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class InteractionPainting : MonoBehaviour
    {
        [SerializeField] GameObject onPainting;
        [SerializeField] ScreenFader screenFader;
        private float fadeTimer;
        private float fadeTime = 0.5f;
        private bool on = false;


        private void Update()
        {
            if (on)
            {
                fadeTimer -= Time.deltaTime;
                if(fadeTimer <= 0 )
                {
                    onPainting.SetActive(true);
                }
            }
        }
        public void OnPainting()
        {
            screenFader.FadeOutToIn();
            fadeTimer = fadeTime;
            on = true;
            //onPainting.SetActive(true);
        }

    }
}
