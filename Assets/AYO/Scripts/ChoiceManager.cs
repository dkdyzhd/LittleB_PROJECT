using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
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
            EndChoice();
        }

        // 선택지 목록을 받아오는 함수 >  플레이어/npc에게서 받아옴
        public void GetChoiceArray(ChoiceArray choiceArray)
        {
            choicearray = choiceArray;
        }

        // 받아온 선택지 목록을 판별하는 함수
        public void CheckChoiceArray()
        {
            int j = 0;
            choiceui.SetChoiceCharacter(choicearray.GetCharacterData().characterSprite, choicearray.GetCharacterData().characterName);

            for(int i = 0; i <choicearray.GetChoiceCount(); i++)
            {
                choice = choicearray.GetChoice(i);

                if (choice.ChoiceCondition())
                {
                    choiceui.SetButtonData(j, tableLoader.GetChoiceData(choice.GetChoiceID()), choice.NextEvent());
                    j++;
                    // GameObject button = choiceui.CreateChoiceButton();
                    // TO do : 매개변수 작성
                    //choiceui.SetChoiceArrayData(choicearray.GetCharacterData().characterSprite, choicearray.GetCharacterData().characterName,
                       // tableLoader.GetChoiceData(choice.GetChoiceID()), choice.NextEvent());
                }
            }

            choiceUI.SetActive(true);
        }

        public void EndChoice()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                choiceUI.SetActive(false);
            }
        }
    }
}
