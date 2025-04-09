using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class ChoiceManager : MonoBehaviour
    {
        [SerializeField] private TextTableLoader tableLoader;
        [SerializeField] private GameObject choiceUI;
        [SerializeField] private Text choiceText;
        [SerializeField] private Image characterImage;

        private Choice choice;
        private string text;
        private string id;

        public void ShowChoice()
        {
            // to do : 상호작용 혹은 다음 이벤트로 불러와서 선택지가 보여지도록 하기
            text = tableLoader.GetChoiceData(choice.GetChoiceID());
        }
    }
}
