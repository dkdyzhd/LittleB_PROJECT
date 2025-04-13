using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Runtime.InteropServices.WindowsRuntime;

namespace AYO
{
    public class ChoiceUI : MonoBehaviour
    {
        [SerializeField] private Text speaker;
        [SerializeField] private Image characterImage;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private Transform choiceListPanel;    //UI������ ����� �� �θ� ������Ʈ
        [SerializeField] private ButtonUI buttonUI;

        public void CreateChoiceButton()
        {
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceListPanel);

            //Text choiceText = choiceButton.GetComponentInChildren<Text>();
            //Button button = choiceButton.GetComponent<Button>();
            // To do : �������� ������ �ִ� �̺�Ʈ�� ��ư onClick���� ����?
            // button.onClick.AddListener(() => );
            // ��ư �ȿ� �ִ� �ؽ�Ʈ�� �̺�Ʈ�� �ٲ���� ��.> ���?
        }   // ��ưUI Ŭ���� ���� ����� >  �ؽ�Ʈ & �̺�Ʈ �޾ƿ��� ��� �����

        public void SetChoiceArrayData(Sprite characterSprite, string characterName, string choiceText, UnityEvent choiceEvent)
        {
            characterImage.sprite = characterSprite;    // �̹��� �ȿ� �ִ� sprite�� �ٲ��ִ� ��
            speaker.text = characterName;
            buttonUI.SetButton(choiceText, choiceEvent);
            //buttonUI.text.text = choiceText;
            //buttonUI.button.onClick.AddListener(() => );

        }
    }
}
