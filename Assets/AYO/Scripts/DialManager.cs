using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class DialManager : MonoBehaviour
    {
        private NPCData npcData;
        private string currentLineID = "LN010100";
        private int i = -1; // i�� ��� �ʱ�ȭ �������

        [SerializeField] private DialogueTableLoader tableLoader;
        [SerializeField] private Text dialogueLine;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // npcData = GetComponent<NPCData>();  >> ��� �����ð�����?
        // lines�� ù��° ���� �����ϴ� ������ ���� �ʿ� X !!
        // �ݺ����� ����ϴ� ��� �Լ��� i++ & Enter �� ���������� ����ǵ���
        public void ShowDialogue()
        {
            // npcData = GetComponent<NPCData>();  >> ��� �����ð�����?
            List<string> lines = tableLoader.GetDialogueData(currentLineID);
            if(i < lines.Count - 1)
            {
                i++;
                dialogueLine.text = lines[i];
            }
        }

        public void StartDialogue()
        {
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
