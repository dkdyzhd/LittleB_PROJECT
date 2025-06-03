using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class ImageToShow : MonoBehaviour
    {
        [SerializeField] ImageDisplayDataSO imageDisplayDataSO;
        private SpriteRenderer spriteRenderer;
        private Text text;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            text = GetComponent<Text>();
        }

        public void ShowImage(ImageDisplayDataSO imageDisplayDataSO)
        {
            spriteRenderer.sprite = imageDisplayDataSO.imageContent;
            if (imageDisplayDataSO.textContent != null)
            {
                text.text = imageDisplayDataSO.textContent;
            }
        }
    }
}
