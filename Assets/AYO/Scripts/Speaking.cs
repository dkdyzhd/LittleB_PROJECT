using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class Speaking : MonoBehaviour
    {
        private string characterName;
        private string lineID;
        private List<string> lines = new List<string>();
        private Sprite sprite;

        private DialogueTableLoader tableLoader;

        private void Start()
        {
            tableLoader = GetComponent<DialogueTableLoader>();
        }

        // 발화 생성
        public void CurrentSpeaking(string characterName, string lineID, List<string> lines, Sprite sprite)
        {
            this.characterName = characterName;
            this.lineID = lineID;
            this.lines = tableLoader.GetDialogueData(lineID);
            this.sprite = sprite;
        }

        // 캐릭터 정보 가져오기
        public void GetCharacterData(CharacterData character)
        {
            character.characterName = characterName;
            character.characterSprite = sprite;
            character.lineID = lineID;
        }
    }
}
