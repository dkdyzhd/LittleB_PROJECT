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

        private int i;  // ���� ���� ����ؾ��� ����� �� ��ȣ
        private int j;  // ���� ���� ����ؾ��� speaking�� ��ȣ
        private Speaking currentSpeaking;
        private List<string> lines;

        private void Start()
        {
            dialogueUI.SetActive(false);
            
        }

        // npcData = GetComponent<NPCData>();  >> ��� �����ð�����?
        // lines�� ù��° ���� �����ϴ� ������ ���� �ʿ� X !!
        // �ݺ����� ����ϴ� ��� �Լ��� i++ & Enter �� ���������� ����ǵ���
        public void ShowDialogue()
        {
            currentSpeaking = speakingArray.GetSpeaking(j);     //.��ȣ�ۿ��ϰ� �ִ� ������ ��ȭ�� ������ ���ΰ�?
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
                // �迭�� ������ UI ��Ȱ��ȭ > j ���
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
