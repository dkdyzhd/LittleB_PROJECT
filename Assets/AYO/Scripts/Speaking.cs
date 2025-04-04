using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class SpeakingData
    {
        public string characterName;   // ĳ�����̸� "�̿�"
        public string lineID;          // ��縦 �ҷ����� ���� lineID "LN010100"
        public List<string> lines;    // ��縮��Ʈ
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

        #region 250404_���� �ڵ�
        //[SerializeField] private DialogueTableLoader tableLoader;

        //private CharacterData characterData;

        //private InteractionNPC npc;

        //private List<SpeakingData> speakingData = new List<SpeakingData>();

        //private void Start()
        //{

        //}

        //public void CurrentSpeaking(CharacterData character)
        //{
        //    // ��ȭ ����
        //    SpeakingData speakingData = new SpeakingData(
        //        character.characterName, 
        //        character.lineID, 
        //        tableLoader.GetDialogueData(character.lineID), 
        //        character.characterSprite
        //        );
        //}

        //public void SetNPC(InteractionNPC newNPC)
        //{
        //    npc = newNPC;   // ����� ��� null�̶�� ��
        //    Debug.Log($"SetNPC ȣ���: {npc}");    // �ٵ� ȣ���� ��
        //}

        //public void MakeSpeak()
        //{
        //    Debug.Log($"MakeSpeak() ȣ���, npc ����: {npc}");

        //    if (npc == null)
        //    {
        //        Debug.LogError("npc�� �Ҵ���� �ʾҽ��ϴ�! ��ȣ�ۿ��� ���� �����ϼ���.");
        //        return;
        //    }

        //    characterData = npc.GetCharacterData();
        //    CurrentSpeaking(characterData);
        //}

        //// ��ȭ ����
        ////public void CurrentSpeaking(string characterName, string lineID, List<string> lines, Sprite sprite)
        ////{
        ////    this.characterName = characterName;
        ////    this.lineID = lineID;
        ////    this.lines = tableLoader.GetDialogueData(lineID);
        ////    this.sprite = sprite;
        ////}

        //// ĳ���� ���� ��������
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
