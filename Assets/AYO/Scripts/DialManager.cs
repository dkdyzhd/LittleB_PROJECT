using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class DialManager : MonoBehaviour
    {

        //[SerializeField] private DialogueUI dialogueUI;
        //[SerializeField] private SpeakingArray firstSpeakingArray;
        [SerializeField] private TextTableLoader tableLoader;
        [SerializeField] private GameObject dialogueUI;
        [SerializeField] private Text dialogueLine;
        [SerializeField] private Image characterImage;

        //[SerializeField] private Speaking speaking;

        private int i;  // 현재 내가 출력해야할 대사의 줄 번호
        private int j;  // 현재 내가 출력해야할 speaking의 번호
        private Speaking currentSpeaking;
        private SpeakingArray currentSpeakingArray;
        private List<string> lines;

        private void Start()
        {
            dialogueUI.SetActive(false);
        }

        // 선택지 이벤트에 넣어줄 함수
        public void ShowDialogue(SpeakingArray speakingArray)
        {
            currentSpeakingArray = speakingArray;
            dialogueUI.SetActive(true);

            ShowDialogue();
        }

        // 상호작용 할 때 첫 인사를 받아오는 함수 >> ShowDialogue 로 받아올 수 있음 / 굳이 필요 X
        //public void FirstSpeakingArray(SpeakingArray speakingArray)
        //{
        //    currentSpeakingArray = speakingArray;
        //}

        // npcData = GetComponent<NPCData>();  >> 어떻게 가져올것인지?
        // lines의 첫번째 줄을 저장하는 변수를 만들 필요 X !!
        // 반복문을 사용하는 대신 함수에 i++ & Enter 를 누를때마다 실행되도록
        public void ShowDialogue()
        {
            currentSpeaking = currentSpeakingArray.GetSpeaking(j);
            lines = tableLoader.GetTextData(currentSpeaking.GetSpeakingID());

            dialogueLine.text = lines[i];

        }

        public void ReadLine()
        {
            if (Input.GetKeyDown(KeyCode.Return) && i < lines.Count -1)
            {
                i++;
                dialogueLine.text = lines[i];
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return) && i >= lines.Count -1)
            {
                j++;
                i = 0;
                // 배열이 끝나면 UI 비활성화 > j 사용
                if(j >= currentSpeakingArray.GetArrayLength())      // firstSpeakingArray
                {
                    j = 0;
                    dialogueUI.SetActive(false);
                    // 다음 이벤트 호출하면서  대화 종료
                    currentSpeakingArray.InvokeNextEvent();
                    return;
                }

                ShowDialogue();

            }
        }

        // Ult 이벤트에서 사용
        public void EndDialogue()
        {
            dialogueUI.SetActive(false);
        }

        public void Escape()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                dialogueUI.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            ReadLine();
            Escape();
        }
    }
}
