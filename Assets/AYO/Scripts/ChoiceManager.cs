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
        //[SerializeField] private ChoiceArray choiceArray;       // ������ ���� �͸� �̷��� ǥ��
        [SerializeField] private GameObject choiceUI;


        [SerializeField] private Text characterName;
        [SerializeField] private Image characterImage;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private Transform choiceListPanel;    //UI������ ����� �� �θ� ������Ʈ

        [SerializeField] private ChoiceUI choiceui;
        private ChoiceArray choicearray;
        private Choice choice;
        private int i;  //�������迭���� �������� ������ �� ���
        private string text;
        private string id;

        private void Start()
        {
            // ������ UI ��Ȱ��ȭ > ���⼭ �ϴ°� �³�?
            choiceUI.SetActive(false);
        }

        // ������ ����� �޾ƿ��� �Լ� >  �÷��̾�/npc���Լ� �޾ƿ�
        public void GetChoiceArray(ChoiceArray choiceArray)
        {
            choicearray = choiceArray;
        }

        // �޾ƿ� ������ ����� �Ǻ��ϴ� �Լ�
        public void CheckChoiceArray()
        {
            for(i = 0; i <choicearray.GetChoiceCount(); i++)
            {
                choice = choicearray.GetChoice(i);
                if (choice.ChoiceCondition())
                {
                    choiceui.CreateChoiceButton();
                    // TO do : �Ű����� �ۼ�
                    choiceui.SetChoiceArrayData(choicearray.GetCharacterData().characterSprite, choicearray.GetCharacterData().characterName,
                        tableLoader.GetChoiceData(choice.GetChoiceID()), choice.NextEvent());
                }
            }

            choiceUI.SetActive(true);
        }

        public void ShowChoice()
        {
            // to do : ��ȣ�ۿ� Ȥ�� ���� �̺�Ʈ�� �ҷ��ͼ� �������� ���������� �ϱ�
            text = tableLoader.GetChoiceData(choice.GetChoiceID());
        }
    }
}
