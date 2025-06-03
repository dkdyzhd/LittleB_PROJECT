using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private Text dialogueLine;
        [SerializeField] private Text speaker;
        [SerializeField] private Image characterImage;

        [SerializeField] private TextTableLoader dialogueTableLoader;

        // Start is called before the first frame update
        void Start()
        {
        
        }
        public void SetDialogueCharacter(Sprite characterSprite, string characterName)
        {
            characterImage.sprite = characterSprite;
            speaker.text = characterName;
        }

        public void ShowDialogue()
        {
            // DialogueTableLoader ���� ����� �����ͼ� text�� ����ֱ� > 
            // ���͸� ġ�� ���� ���� �Ѿ����
            // npc�� ��ȣ�ۿ��� �ϸ� lineID�� ������ �� �ֵ��� -> �ҷ�����?
            // npc�� lineID�� ������ �־����
            // NpcData ���� (ScriptableObject �� �����) -> ��ȣ�ۿ��ϸ� Data(string lineID) �о����
            // �о�� lineID�� DialogueTableLoader.GetDialogueData(string lineID) �� �ְ� string �ҷ�����
            // �ҷ��� string �� text�� ��ȯ
            // ��ȯ�� text�� �� �� �� �����ֱ�
            // ���� / �����̽��� ������ ���� ���� �Ѿ����
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
