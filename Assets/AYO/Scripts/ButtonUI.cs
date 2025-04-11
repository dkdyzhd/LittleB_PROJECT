using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    }
}
