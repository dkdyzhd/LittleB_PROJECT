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
            // ������ UI ��Ȱ��ȭ > ���⼭ �ϴ°� �³�?
            choiceUI.SetActive(false);
        }

        private void Update()
        {
            EndChoice();
        }

        // ������ ����� �޾ƿ��� �Լ� >  �÷��̾�/npc���Լ� �޾ƿ�
        public void GetChoiceArray(ChoiceArray choiceArray)
        {
            choicearray = choiceArray;
        }

        // �޾ƿ� ������ ����� �Ǻ��ϴ� �Լ�
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
                    // TO do : �Ű����� �ۼ�
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
