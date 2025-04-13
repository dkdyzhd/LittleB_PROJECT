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
        //[SerializeField] private ChoiceArray choiceArray;       // 변하지 않을 것만 이렇게 표시
        [SerializeField] private GameObject choiceUI;


        [SerializeField] private Text characterName;
        [SerializeField] private Image characterImage;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private Transform choiceListPanel;    //UI아이템 목록이 들어갈 부모 오브젝트

        [SerializeField] private ChoiceUI choiceui;
        private ChoiceArray choicearray;
        private Choice choice;
        private int i;  //선택지배열에서 선택지를 가져올 때 사용
        private string text;
        private string id;

        private void Start()
        {
            // 선택지 UI 비활성화 > 여기서 하는게 맞나?
            choiceUI.SetActive(false);
        }

        // 선택지 목록을 받아오는 함수 >  플레이어/npc에게서 받아옴
        public void GetChoiceArray(ChoiceArray choiceArray)
        {
            choicearray = choiceArray;
        }

        // 받아온 선택지 목록을 판별하는 함수
        public void CheckChoiceArray()
        {
            for(i = 0; i <choicearray.GetChoiceCount(); i++)
            {
                choice = choicearray.GetChoice(i);
                if (choice.ChoiceCondition())
                {
                    choiceui.CreateChoiceButton();
                    // TO do : 매개변수 작성
                    choiceui.SetChoiceArrayData(choicearray.GetCharacterData().characterSprite, choicearray.GetCharacterData().characterName,
                        tableLoader.GetChoiceData(choice.GetChoiceID()), choice.NextEvent());
                }
            }

            choiceUI.SetActive(true);
        }

        public void ShowChoice()
        {
            // to do : 상호작용 혹은 다음 이벤트로 불러와서 선택지가 보여지도록 하기
            text = tableLoader.GetChoiceData(choice.GetChoiceID());
        }
    }
}
