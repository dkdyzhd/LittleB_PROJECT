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
        [SerializeField] private Text characterName;
        [SerializeField] private Image characterImage;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private Transform choiceListPanel;    //UI������ ����� �� �θ� ������Ʈ

        public void CreateChoiceButton()
        {
            //To do : �������� ������ �����ͼ� ���̸� Instactiate
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceListPanel);

            Text choiceText = choiceButton.GetComponentInChildren<Text>();

            Button button = choiceButton.GetComponent<Button>();
            // ��ư �ȿ� �ִ� �ؽ�Ʈ�� �̺�Ʈ�� �ٲ���� ��.> ���?
        }   // ��ưUI Ŭ���� ���� ����� >  �ؽ�Ʈ & �̺�Ʈ �޾ƿ��� ��� �����

        public void GetChoiceArrayData(Sprite characterImage, string characterName, string choiceText, UnityEvent choiceEvent)
        {
            
        }
    }
}
