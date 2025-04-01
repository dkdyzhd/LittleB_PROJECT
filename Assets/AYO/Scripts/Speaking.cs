using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class SpeakingData
    {
        public string characterName;   // ĳ�����̸� "�̿�"
        public string lineID;          // ��縦 �ҷ����� ���� lineID "LN010100"
        public List<string> lines = new List<string>();    // ��縮��Ʈ
        public Sprite sprite;          // ĳ���� �ʻ�ȭ

        // Ŭ���� ������
        public SpeakingData(string characterName, string lineID, List<string> lines, Sprite sprite)
        {
            this.characterName = characterName;
            this.lineID = lineID;
            this.lines = lines;
            this.sprite = sprite;
        }
    }

    public class Speaking : MonoBehaviour
    {// Ŭ�����̸� �����ϱ� - ����ϰ� �ִ� ������ �� �˾ƺ�����
        
        //private string characterName;   // ĳ�����̸� "�̿�"
        //private string lineID;          // ��縦 �ҷ����� ���� lineID "LN010100"
        //private List<string> lines = new List<string>();    // ��縮��Ʈ
        //private Sprite sprite;          // ĳ���� �ʻ�ȭ

        private DialogueTableLoader tableLoader;

        private List<SpeakingData> speakingData = new List<SpeakingData>();

        private void Start()
        {
            tableLoader = GetComponent<DialogueTableLoader>();
        }


        public void CurrentSpeaking(CharacterData character)
        {
            // ��ȭ ����
            SpeakingData speakingData = new SpeakingData(
                character.characterName, 
                character.lineID, 
                tableLoader.GetDialogueData(character.lineID), 
                character.characterSprite
                );
        }

        // ��ȭ ����
        //public void CurrentSpeaking(string characterName, string lineID, List<string> lines, Sprite sprite)
        //{
        //    this.characterName = characterName;
        //    this.lineID = lineID;
        //    this.lines = tableLoader.GetDialogueData(lineID);
        //    this.sprite = sprite;
        //}

        // ĳ���� ���� ��������
        //public void GetCharacterData(CharacterData character)
        //{
        //    character.characterName = characterName;
        //    character.characterSprite = sprite;
        //    character.lineID = lineID;
        //}
    }
}
