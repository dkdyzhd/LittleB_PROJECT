using AYO.InputInterface;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace AYO
{
    public class DialManager : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private PlayerInputEventManager pInputManager;
        [SerializeField] private TextTableLoader tableLoader;
        [SerializeField] private GameObject dialogueUI;
        [SerializeField] private Text dialogueLine;
        [SerializeField] private Image characterImage;
        [SerializeField] private DialogueUI dialogueui; // DialogueUI.cs 레퍼런스

        //[SerializeField] private Speaking speaking;

        private int i;  // 현재 내가 출력해야할 대사의 줄 번호
        private int j;  // 현재 내가 출력해야할 speaking의 번호
        private Speaking currentSpeaking;
        private SpeakingArray currentSpeakingArray;
        private List<string> lines;
        private CharacterData currentCharData; // 현재 진행중인 Speaking의 캐릭터 데이터

        public bool IsCurrentlyActive { get; private set; } // 외부에서 읽기 전용 by 휘익 250521
        public bool IsDialogueAndEventsFullyCompleted { get; private set; } // 후속 이벤트까지 끝났는지 여부 플래그 by 휘익 250521


        private void Start()
        {
            dialogueUI.SetActive(false);
        }

        // 선택지 이벤트에 넣어줄 함수
        public void ShowDialogue(SpeakingArray speakingArray)
        {
            pInputManager.NavigateTarget = null;
            pInputManager.LeftClickTarget = null;
            pInputManager.OnInventoryTarget = null;
            player.SetControl(false); // by 휘익 250526

            currentSpeakingArray = speakingArray;
            dialogueUI.SetActive(true);

            IsDialogueAndEventsFullyCompleted = false; // 대화 시작 시 초기화 by 휘익 250521
            IsCurrentlyActive = true; // 대화 시작 시 true by 휘익 250521

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
            currentCharData = currentSpeaking.GetCharacterData();
            lines = tableLoader.GetTextData(currentSpeaking.GetSpeakingID());
            dialogueui.SetDialogueCharacter(currentCharData.characterSprite, currentCharData.characterName);

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
                    IsCurrentlyActive = false; // 정상 종료 시 false by 휘익 250521

                    // 다음 이벤트 호출하면서  대화 종료
                    currentSpeakingArray.InvokeNextEvent();

                    // InvokeNextEvent 후 모든 것이 완료되었음을 알림 by 휘익 250521
                    IsDialogueAndEventsFullyCompleted = true;
                    Debug.Log("DialManager: Dialogue UI closed and NextEvent invoked. Fully completed.");

                    return;
                }

                ShowDialogue();

            }
        }

        // Ult 이벤트에서 사용
        public void EndDialogue()
        {
            dialogueUI.SetActive(false);

            IsCurrentlyActive = false; // 외부 호출로 종료 시 false by 휘익 250521
            IsDialogueAndEventsFullyCompleted = true; // 강제 종료 시에도 완료로 간주 by 휘익 250521

            if (pInputManager == null)
            {
                Debug.LogError("pInputManager IS NULL!");
            }
            else
            {
                pInputManager.NavigateTarget = player;
                pInputManager.LeftClickTarget = player;
                pInputManager.OnInventoryTarget = player;
            }

            // 3. player.SetControl(true) 호출 직전 player 상태 확인
            if (player == null)
            {
                Debug.LogError("Player IS NULL before calling SetControl(true)!");
            }
            else
            {
                player.SetControl(true); // 이 라인이 실행되면 PlayerController의 로그가 떠야 함
            }
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
