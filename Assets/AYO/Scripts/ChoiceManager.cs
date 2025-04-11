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
        [SerializeField] private ChoiceArray choiceArray;       // 변하지 않을 것만 이렇게 표시
        [SerializeField] private GameObject choiceUI;


        [SerializeField] private Text characterName;
        [SerializeField] private Image characterImage;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private Transform choiceListPanel;    //UI아이템 목록이 들어갈 부모 오브젝트

        private ChoiceUI choiceui;
        private ChoiceArray choicearray;
        private Choice choice;
        private int i;  //선택지배열에서 선택지를 가져올 때 사용
        private string text;
        private string id;

        private void Start()
        {
            // 선택지 UI 비활성화
            choiceUI.SetActive(false);
        }

        // 선택지 목록을 받아오는 함수
        public void GetChoiceArray(ChoiceArray choiceArray)
        {
            choicearray = choiceArray;
        }

        // 받아온 선택지 목록을 판별하는 함수
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
            //To do : 선택지의 조건을 가져와서 참이면 Instactiate
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceListPanel);

            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = tableLoader.GetChoiceData(choice.GetChoiceID());

            Button button = choiceButton.GetComponent<Button>();
            // To do : 선택지가 가지고 있는 이벤트를 버튼 onClick으로 대입?
            // button.onClick.AddListener(() => );
        }

        public void ShowChoice()
        {
            // to do : 상호작용 혹은 다음 이벤트로 불러와서 선택지가 보여지도록 하기
            text = tableLoader.GetChoiceData(choice.GetChoiceID());
        }
    }
}
