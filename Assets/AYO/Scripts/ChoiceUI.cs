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
        [SerializeField] private ButtonUI[] buttonUIArray;

        private void Start()
        {
            
        }

        // ���� ���� ������ ���� ����
        public void SetChoiceCharacter(Sprite characterSprite, string characterName)
        {
            characterImage.sprite = characterSprite;    // �̹��� �ȿ� �ִ� sprite�� �ٲ��ִ� ��
            speaker.text = characterName;
        }

        // �� �� ° ��ư�� �����͸� �־��� ������ 
        public void SetButtonData(int j, string choiceText, UnityAction choiceEvent)
        {
            buttonUIArray[j].SetButton(choiceText, choiceEvent);
            buttonUIArray[j].gameObject.SetActive(true);
        }

        //[SerializeField] private Transform choiceListPanel;    //UI������ ����� �� �θ� ������Ʈ
        //[SerializeField] private GameObject choiceButtonPrefab;
        // ��ư�� �̸� ���� ����� ���̱� ������ �ּ�ó�� > �������� ������ �������� ���� �ʾƼ�
        //public GameObject CreateChoiceButton()
        //{
        //    GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceListPanel);
        //    return choiceButton;
        //}   

    }
}
