using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UltEvents;
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
        public void SetButtonData(int j, string choiceText, UltEvent choiceEvent)    //UltEvent �� �ٲٸ� UnityAction �� �ȵ�
        {
            buttonUIArray[j].SetButton(choiceText, choiceEvent);
            buttonUIArray[j].gameObject.SetActive(true);
        }

        public void ResetButton()
        {
            for(int i = 0; i < buttonUIArray.Length; i++) 
            { 
                buttonUIArray[i].gameObject.SetActive(false);
                buttonUIArray[i].SetButton(null, null);
            }
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
