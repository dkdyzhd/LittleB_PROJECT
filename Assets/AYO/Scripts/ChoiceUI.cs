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

        // 누가 가진 선택지 인지 세팅
        public void SetChoiceCharacter(Sprite characterSprite, string characterName)
        {
            characterImage.sprite = characterSprite;    // 이미지 안에 있는 sprite를 바꿔주는 것
            speaker.text = characterName;
        }

        // 몇 번 째 버튼에 데이터를 넣어줄 것인지 
        public void SetButtonData(int j, string choiceText, UltEvent choiceEvent)    //UltEvent 로 바꾸면 UnityAction 이 안됨
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

        //[SerializeField] private Transform choiceListPanel;    //UI아이템 목록이 들어갈 부모 오브젝트
        //[SerializeField] private GameObject choiceButtonPrefab;
        // 버튼을 미리 만들어서 사용할 것이기 때문에 주석처리 > 선택지의 갯수의 변동폭이 넓지 않아서
        //public GameObject CreateChoiceButton()
        //{
        //    GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceListPanel);
        //    return choiceButton;
        //}   

    }
}
