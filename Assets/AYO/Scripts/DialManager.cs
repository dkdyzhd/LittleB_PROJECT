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

        private int i;  // ���� ���� ����ؾ��� ����� �� ��ȣ
        private int j;  // ���� ���� ����ؾ��� speaking�� ��ȣ
        private Speaking currentSpeaking;
        private SpeakingArray currentSpeakingArray;
        private List<string> lines;

        private void Start()
        {
            dialogueUI.SetActive(false);
        }

        // ������ �̺�Ʈ�� �־��� �Լ�
        public void ShowDialogue(SpeakingArray speakingArray)
        {
            currentSpeakingArray = speakingArray;
            dialogueUI.SetActive(true);

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
                // �迭�� ������ UI ��Ȱ��ȭ > j ���
                if(j >= currentSpeakingArray.GetArrayLength())      // firstSpeakingArray
                {
                    j = 0;
                    dialogueUI.SetActive(false);
                    // ���� �̺�Ʈ ȣ���ϸ鼭  ��ȭ ����
                    currentSpeakingArray.InvokeNextEvent();
                    return;
                }

                ShowDialogue();

            }
        }

        // Ult �̺�Ʈ���� ���
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
