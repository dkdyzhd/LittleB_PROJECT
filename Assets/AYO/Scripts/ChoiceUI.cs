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
        [SerializeField] private Transform choiceListPanel;    //UI아이템 목록이 들어갈 부모 오브젝트
        [SerializeField] private ButtonUI buttonUI;

        public void CreateChoiceButton()
        {
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceListPanel);

            //Text choiceText = choiceButton.GetComponentInChildren<Text>();
            //Button button = choiceButton.GetComponent<Button>();
            // To do : 선택지가 가지고 있는 이벤트를 버튼 onClick으로 대입?
            // button.onClick.AddListener(() => );
            // 버튼 안에 있는 텍스트와 이벤트를 바꿔줘야 함.> 어떻게?
        }   // 버튼UI 클래스 따로 만들기 >  텍스트 & 이벤트 받아오는 기능 만들기

        public void SetChoiceArrayData(Sprite characterSprite, string characterName, string choiceText, UnityEvent choiceEvent)
        {
            characterImage.sprite = characterSprite;    // 이미지 안에 있는 sprite를 바꿔주는 것
            speaker.text = characterName;
            buttonUI.SetButton(choiceText, choiceEvent);
            //buttonUI.text.text = choiceText;
            //buttonUI.button.onClick.AddListener(() => );

        }
    }
}
