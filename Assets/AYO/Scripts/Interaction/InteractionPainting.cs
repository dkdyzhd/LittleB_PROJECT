using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class InteractionPainting : MonoBehaviour
    {
        [SerializeField] GameObject onPainting;
        [SerializeField] ScreenFader screenFader;


        public void OnPainting()
        {
            screenFader.ScreenFadeOut();
            onPainting.SetActive(true);
            screenFader.ScreenFadeIn();
        }

    }
}
