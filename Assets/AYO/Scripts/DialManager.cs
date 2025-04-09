using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class DialManager : MonoBehaviour
    {

        //[SerializeField] private DialogueUI dialogueUI;
        [SerializeField] private SpeakingArray speakingArray;
        [SerializeField] private TextTableLoader tableLoader;
        [SerializeField] private GameObject dialogueUI;
        [SerializeField] private Text dialogueLine;
        [SerializeField] private Image characterImage;

        //[SerializeField] private Speaking speaking;

        private int i;  // 현재 내가 출력해야할 대사의 줄 번호
        private int j;  // 현재 내가 출력해야할 speaking의 번호
        private Speaking currentSpeaking;
        private List<string> lines;

        private void Start()
        {
            dialogueUI.SetActive(false);
            
        }

        // npcData = GetComponent<NPCData>();  >> 어떻게 가져올것인지?
        // lines의 첫번째 줄을 저장하는 변수를 만들 필요 X !!
        // 반복문을 사용하는 대신 함수에 i++ & Enter 를 누를때마다 실행되도록
        public void ShowDialogue()
        {
            currentSpeaking = speakingArray.GetSpeaking(j);     //.상호작용하고 있는 대상과의 대화가 나오는 것인가?
            //List<string> lines = tableLoader.GetDialogueData(currentSpeaking.GetID()); 
            lines = tableLoader.GetTextData(currentSpeaking.GetSpeakingID());

            dialogueLine.text = lines[i];

        }

        public void ReadLine()
        {
            if (Input.GetKeyDown(KeyCode.Return) && i < lines.Count -1)
            {
                i++;
                dialogueLine.text = lines[i];
            }

            if (Input.GetKeyDown(KeyCode.Return) && i >= lines.Count -1)
            {
                j++;
                i = 0;
                // 배열이 끝나면 UI 비활성화 > j 사용
                if(j >= speakingArray.GetArrayLength())
                {
                    dialogueUI.SetActive(false);
                    speakingArray.InvokeNextEvent();
                    return;
                }

                ShowDialogue();

            }
        }

        public void StartDialogue()
        {
            dialogueUI.SetActive(true);
            //speaking.MakeSpeak();

            ShowDialogue();
            
        }

        public void EndDialogue()
        {
            dialogueUI.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            ReadLine();
        }
    }
}
