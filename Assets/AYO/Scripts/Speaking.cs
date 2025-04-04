using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class SpeakingData
    {
        public string characterName;   // 캐릭터이름 "미오"
        public string lineID;          // 대사를 불러오기 위한 lineID "LN010100"
        public List<string> lines;    // 대사리스트
        public Sprite sprite;          // 캐릭터 초상화

        // 클래스 생성자
        public SpeakingData(string characterName, string lineID, List<string> lines, Sprite sprite)
        {
            this.characterName = characterName;
            this.lineID = lineID;
            this.lines = lines;
            this.sprite = sprite;
        }
    }

    public class Speaking : MonoBehaviour
    {// 클래스이름 변경하기 - 담당하고 있는 역할을 잘 알아보도록

        #region 250404_기존 코드
        //[SerializeField] private DialogueTableLoader tableLoader;

        //private CharacterData characterData;

        //private InteractionNPC npc;

        //private List<SpeakingData> speakingData = new List<SpeakingData>();

        //private void Start()
        //{

        //}

        //public void CurrentSpeaking(CharacterData character)
        //{
        //    // 발화 생성
        //    SpeakingData speakingData = new SpeakingData(
        //        character.characterName, 
        //        character.lineID, 
        //        tableLoader.GetDialogueData(character.lineID), 
        //        character.characterSprite
        //        );
        //}

        //public void SetNPC(InteractionNPC newNPC)
        //{
        //    npc = newNPC;   // 디버깅 결과 null이라고 뜸
        //    Debug.Log($"SetNPC 호출됨: {npc}");    // 근데 호출이 됨
        //}

        //public void MakeSpeak()
        //{
        //    Debug.Log($"MakeSpeak() 호출됨, npc 상태: {npc}");

        //    if (npc == null)
        //    {
        //        Debug.LogError("npc가 할당되지 않았습니다! 상호작용을 먼저 수행하세요.");
        //        return;
        //    }

        //    characterData = npc.GetCharacterData();
        //    CurrentSpeaking(characterData);
        //}

        //// 발화 생성
        ////public void CurrentSpeaking(string characterName, string lineID, List<string> lines, Sprite sprite)
        ////{
        ////    this.characterName = characterName;
        ////    this.lineID = lineID;
        ////    this.lines = tableLoader.GetDialogueData(lineID);
        ////    this.sprite = sprite;
        ////}

        //// 캐릭터 정보 가져오기
        ////public void GetCharacterData(CharacterData character)
        ////{
        ////    character.characterName = characterName;
        ////    character.characterSprite = sprite;
        ////    character.lineID = lineID;
        ////}
        #endregion

        [SerializeField] private CharacterData characterData;
        [SerializeField] private string id;

        public string GetID()
        {
            return id;
        }
    }
}
