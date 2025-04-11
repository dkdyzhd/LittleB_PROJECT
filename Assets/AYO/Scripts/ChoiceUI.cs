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
        [SerializeField] private Transform choiceListPanel;    //UI아이템 목록이 들어갈 부모 오브젝트

        public void CreateChoiceButton()
        {
            //To do : 선택지의 조건을 가져와서 참이면 Instactiate
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceListPanel);

            Text choiceText = choiceButton.GetComponentInChildren<Text>();

            Button button = choiceButton.GetComponent<Button>();
            // 버튼 안에 있는 텍스트와 이벤트를 바꿔줘야 함.> 어떻게?
        }   // 버튼UI 클래스 따로 만들기 >  텍스트 & 이벤트 받아오는 기능 만들기

        public void GetChoiceArrayData(Sprite characterImage, string characterName, string choiceText, UnityEvent choiceEvent)
        {
            
        }
    }
}
