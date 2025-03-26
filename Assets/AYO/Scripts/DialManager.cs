using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class DialManager : MonoBehaviour
    {
        private NPCData npcData;
        private string currentLineID = "LN010100";
        private int i = -1; // i를 계속 초기화 해줘야함

        [SerializeField] private DialogueTableLoader tableLoader;
        [SerializeField] private Text dialogueLine;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // npcData = GetComponent<NPCData>();  >> 어떻게 가져올것인지?
        // lines의 첫번째 줄을 저장하는 변수를 만들 필요 X !!
        // 반복문을 사용하는 대신 함수에 i++ & Enter 를 누를때마다 실행되도록
        public void ShowDialogue()
        {
            // npcData = GetComponent<NPCData>();  >> 어떻게 가져올것인지?
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
                Debug.Log("입력");
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
