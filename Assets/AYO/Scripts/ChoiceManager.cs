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
        [SerializeField] private ChoiceArray choiceArray;       // ������ ���� �͸� �̷��� ǥ��
        [SerializeField] private GameObject choiceUI;


        [SerializeField] private Text characterName;
        [SerializeField] private Image characterImage;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private Transform choiceListPanel;    //UI������ ����� �� �θ� ������Ʈ

        private ChoiceUI choiceui;
        private ChoiceArray choicearray;
        private Choice choice;
        private int i;  //�������迭���� �������� ������ �� ���
        private string text;
        private string id;

        private void Start()
        {
            // ������ UI ��Ȱ��ȭ
            choiceUI.SetActive(false);
        }

        // ������ ����� �޾ƿ��� �Լ�
        public void GetChoiceArray(ChoiceArray choiceArray)
        {
            choicearray = choiceArray;
        }

        // �޾ƿ� ������ ����� �Ǻ��ϴ� �Լ�
        public void CheckChoiceArray(int i)
        {

            choice = choicearray.GetChoice(i);
            if (choice.ChoiceCondition())
            {
                choiceui.CreateChoiceButton();
                i++;
            }
            else
            {
                i++;
            }
        }

        public void CreateChoiceButton()
        {
            //choice = choiceArray.GetChoice(i);
            //To do : �������� ������ �����ͼ� ���̸� Instactiate
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceListPanel);

            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = tableLoader.GetChoiceData(choice.GetChoiceID());

            Button button = choiceButton.GetComponent<Button>();
            // To do : �������� ������ �ִ� �̺�Ʈ�� ��ư onClick���� ����?
            // button.onClick.AddListener(() => );
        }

        public void ShowChoice()
        {
            // to do : ��ȣ�ۿ� Ȥ�� ���� �̺�Ʈ�� �ҷ��ͼ� �������� ���������� �ϱ�
            text = tableLoader.GetChoiceData(choice.GetChoiceID());
        }
    }
}
