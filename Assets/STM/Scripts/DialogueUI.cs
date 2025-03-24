using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private Text dialogueLine;

        [SerializeField] private DialogueTableLoader dialogueTableLoader;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        public void ShowDialogue()
        {
            // DialogueTableLoader 에서 기능을 가져와서 text에 집어넣기
            // 엔터를 치면 다음 대사로 넘어가도록
            // npc와 상호작용을 하면 lineID를 가져올 수 있도록 -> 불러오기?
            // npc가 lineID를 가지고 있어야함
            // NpcData 생성 (ScriptableObject 로 만들기) -> 상호작용하면 Data(string lineID) 읽어오기
            // 읽어온 lineID를 DialogueTableLoader.GetDialogueData(string lineID) 에 넣고 string 불러오기
            // 불러온 string 을 text로 변환
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
