using System;

namespace AYO
{
    [Serializable]
    public class Dialogue
    {
        public string lineID;         // "LN010100"
        public string dialogueType;   // "0"=NPC, "1"=�÷��̾�
        public string charName;       // ��: "�̿�"
        public string charPortrait;   // ��: "mio.png"
        public string[] lines;        // �� lineID �ȿ� ���� ��
        public string nextLine;       // ���� ����ID or "1" or "-1"

        public Dialogue(string lineID, string dialogueType, string charName,
                        string charPortrait, string[] lines, string nextLine)
        {
            this.lineID = lineID;
            this.dialogueType = dialogueType;
            this.charName = charName;
            this.charPortrait = charPortrait;
            this.lines = lines;
            this.nextLine = nextLine;
        }
    }
}
