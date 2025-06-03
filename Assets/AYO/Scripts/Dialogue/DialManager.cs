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
        [SerializeField] private DialogueUI dialogueui; // DialogueUI.cs ���۷���

        //[SerializeField] private Speaking speaking;

        private int i;  // ���� ���� ����ؾ��� ����� �� ��ȣ
        private int j;  // ���� ���� ����ؾ��� speaking�� ��ȣ
        private Speaking currentSpeaking;
        private SpeakingArray currentSpeakingArray;
        private List<string> lines;
        private CharacterData currentCharData; // ���� �������� Speaking�� ĳ���� ������

        public bool IsCurrentlyActive { get; private set; } // �ܺο��� �б� ���� by ���� 250521
        public bool IsDialogueAndEventsFullyCompleted { get; private set; } // �ļ� �̺�Ʈ���� �������� ���� �÷��� by ���� 250521


        private void Start()
        {
            dialogueUI.SetActive(false);
        }

        // ������ �̺�Ʈ�� �־��� �Լ�
        public void ShowDialogue(SpeakingArray speakingArray)
        {
            pInputManager.NavigateTarget = null;
            pInputManager.LeftClickTarget = null;
            pInputManager.OnInventoryTarget = null;
            player.SetControl(false); // by ���� 250526

            currentSpeakingArray = speakingArray;
            dialogueUI.SetActive(true);

            IsDialogueAndEventsFullyCompleted = false; // ��ȭ ���� �� �ʱ�ȭ by ���� 250521
            IsCurrentlyActive = true; // ��ȭ ���� �� true by ���� 250521

            ShowDialogue();
        }

        // ��ȣ�ۿ� �� �� ù �λ縦 �޾ƿ��� �Լ� >> ShowDialogue �� �޾ƿ� �� ���� / ���� �ʿ� X
        //public void FirstSpeakingArray(SpeakingArray speakingArray)
        //{
        //    currentSpeakingArray = speakingArray;
        //}

        // npcData = GetComponent<NPCData>();  >> ��� �����ð�����?
        // lines�� ù��° ���� �����ϴ� ������ ���� �ʿ� X !!
        // �ݺ����� ����ϴ� ��� �Լ��� i++ & Enter �� ���������� ����ǵ���
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
                // �迭�� ������ UI ��Ȱ��ȭ > j ���
                if(j >= currentSpeakingArray.GetArrayLength())      // firstSpeakingArray
                {
                    j = 0;
                    dialogueUI.SetActive(false);
                    IsCurrentlyActive = false; // ���� ���� �� false by ���� 250521

                    // ���� �̺�Ʈ ȣ���ϸ鼭  ��ȭ ����
                    currentSpeakingArray.InvokeNextEvent();

                    // InvokeNextEvent �� ��� ���� �Ϸ�Ǿ����� �˸� by ���� 250521
                    IsDialogueAndEventsFullyCompleted = true;
                    Debug.Log("DialManager: Dialogue UI closed and NextEvent invoked. Fully completed.");

                    return;
                }

                ShowDialogue();

            }
        }

        // Ult �̺�Ʈ���� ���
        public void EndDialogue()
        {
            dialogueUI.SetActive(false);

            IsCurrentlyActive = false; // �ܺ� ȣ��� ���� �� false by ���� 250521
            IsDialogueAndEventsFullyCompleted = true; // ���� ���� �ÿ��� �Ϸ�� ���� by ���� 250521

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

            // 3. player.SetControl(true) ȣ�� ���� player ���� Ȯ��
            if (player == null)
            {
                Debug.LogError("Player IS NULL before calling SetControl(true)!");
            }
            else
            {
                player.SetControl(true); // �� ������ ����Ǹ� PlayerController�� �αװ� ���� ��
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
