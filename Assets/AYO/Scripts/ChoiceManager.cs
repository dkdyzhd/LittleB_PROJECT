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
            // ������ UI ��Ȱ��ȭ > ���⼭ �ϴ°� �³�?
            choiceUI.SetActive(false);
        }

        private void Update()
        {
            EscapeChoice();
        }

        // ������ ����� �޾ƿ��� �Ǻ��ϴ� �Լ� (Ult�̺�Ʈ���� ���) >  �÷��̾�/npc���Լ� �޾ƿ�
        public void ShowChoiceArray(ChoiceArray choiceArray)
        {
            choicearray = choiceArray;

            int j = 0;
            choiceui.SetChoiceCharacter(choicearray.GetCharacterData().characterSprite, choicearray.GetCharacterData().characterName);

            for (int i = 0; i < choicearray.GetChoiceCount(); i++)
            {
                choice = choicearray.GetChoice(i);

                // Condition �ϳ� �� �� ����� �ڵ�
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
