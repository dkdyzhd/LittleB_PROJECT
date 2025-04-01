using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class DialManager : MonoBehaviour
    {
        private CharacterData characterData;
        private string currentLineID = "LN010100";
        private int i = -1; // i�� ��� �ʱ�ȭ �������

        //[SerializeField] private DialogueUI dialogueUI;
        [SerializeField] private GameObject dialogueUI;
        [SerializeField] private DialogueTableLoader tableLoader;
        [SerializeField] private Text dialogueLine;
        [SerializeField] private Image characterImage;

        private Speaking speaking;
        private SpeakingData speakingData;

        private void Start()
        {
            dialogueUI.SetActive(false);
        }

        // npcData = GetComponent<NPCData>();  >> ��� �����ð�����?
        // lines�� ù��° ���� �����ϴ� ������ ���� �ʿ� X !!
        // �ݺ����� ����ϴ� ��� �Լ��� i++ & Enter �� ���������� ����ǵ���
        public void ShowDialogue()
        {
            // npcData = GetComponent<NPCData>();  >> ��� �����ð�����?
            // List<string> lines = tableLoader.GetDialogueData(currentLineID);
            List<string> lines = speakingData.lines;    //�׽�Ʈ�غ�����
            if (i < lines.Count - 1)
            {
                i++;
                dialogueLine.text = lines[i];
            }
        }

        public void StartDialogue()
        {
            speaking.CurrentSpeaking(characterData);    // �׽�Ʈ�غ�����
            dialogueUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("�Է�");
                ShowDialogue();
            }
        }

        // Update is called once per frame
        void Update()
        {
            StartDialogue();
        }
    }
}
