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
        [SerializeField] private Transform choiceListPanel;    //UI아이템 목록이 들어갈 부모 오브젝트

        private Choice choice;
        private string text;
        private string id;

        private void Start()
        {
            // 선택지 UI 비활성화
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
            // to do : 상호작용 혹은 다음 이벤트로 불러와서 선택지가 보여지도록 하기
            text = tableLoader.GetChoiceData(choice.GetChoiceID());
        }
    }
}
