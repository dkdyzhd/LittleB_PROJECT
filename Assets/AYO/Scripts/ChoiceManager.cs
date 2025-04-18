using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AYO
{
    public class ChoiceManager : MonoBehaviour
    {
        [SerializeField] private TextTableLoader tableLoader;
        [SerializeField] private GameObject choiceUI;
        [SerializeField] private ChoiceUI choiceui;

        private ChoiceArray choicearray;
        private Choice choice;
        private string text;
        private string id;
       
        private void Start()
        {
            // 선택지 UI 비활성화 > 여기서 하는게 맞나?
            choiceUI.SetActive(false);
        }

        private void Update()
        {
            EscapeChoice();
        }

        // 선택지 목록을 받아오고 판별하는 함수 (Ult이벤트에서 사용) >  플레이어/npc에게서 받아옴
        public void ShowChoiceArray(ChoiceArray choiceArray)
        {
            choicearray = choiceArray;

            int j = 0;
            choiceui.SetChoiceCharacter(choicearray.GetCharacterData().characterSprite, choicearray.GetCharacterData().characterName);

            for (int i = 0; i < choicearray.GetChoiceCount(); i++)
            {
                choice = choicearray.GetChoice(i);

                // Condition 하나 일 때 사용한 코드
                //if (choice.ChoiceCondition())
                //{
                //    choiceui.SetButtonData(j, tableLoader.GetChoiceData(choice.GetChoiceID()), choice.NextEvent());
                //    j++;
                //}

                if (choice.ChoiceConditions())
                {
                    choiceui.SetButtonData(j, tableLoader.GetChoiceData(choice.GetChoiceID()), choice.NextEvent());
                    j++;
                }
            }

            choiceUI.SetActive(true);
        }

        public void EndChoiceUI()
        {
            choiceUI.SetActive(false);
        }

        public void EscapeChoice()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                choiceUI.SetActive(false);
            }
        }
    }
}
