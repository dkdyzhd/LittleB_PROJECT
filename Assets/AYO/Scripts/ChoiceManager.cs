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
        [SerializeField] private Text characterName;
        [SerializeField] private Image characterImage;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private Transform choiceListPanel;    //UI������ ����� �� �θ� ������Ʈ

        private Choice choice;
        private string text;
        private string id;

        private void Start()
        {
            // ������ UI ��Ȱ��ȭ
            choiceUI.SetActive(false);
        }

        public void CreateChoiceButton()
        {
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceListPanel);
            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            Button button = choiceButton.GetComponent<Button>();
            // button.onClick.AddListener(() => );
        }

        public void ShowChoice()
        {
            // to do : ��ȣ�ۿ� Ȥ�� ���� �̺�Ʈ�� �ҷ��ͼ� �������� ���������� �ϱ�
            text = tableLoader.GetChoiceData(choice.GetChoiceID());
        }
    }
}
