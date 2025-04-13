using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace AYO
{
    public class ButtonUI : MonoBehaviour
    {
        public Text text;
        public Button button;

        private void Start()
        {
            text = GetComponentInChildren<Text>();
            button = GetComponent<Button>();
        }

        public void SetButton(string buttonText, UnityEvent buttonEvent)
        {
            text.text = buttonText;
        }
    }
}
