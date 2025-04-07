using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class TextTableLoader : MonoBehaviour
    {
        [SerializeField] private TextAsset dialogueTable;
        [SerializeField] private TextAsset choiceTable;

        private Dictionary<string, List<string>> dialogueDictionary = new Dictionary<string, List<string>>();

        // Start is called before the first frame update
        void Start()
        {
            LoadTextTable(dialogueTable);
            LoadTextTable(choiceTable);   
        }

        public void LoadTextTable(TextAsset textAsset)
        {
            StringReader sr = new StringReader(textAsset.text);
            string readline = sr.ReadLine();
            string currentLineID = string.Empty;

            while (!string.IsNullOrEmpty(readline))
            {
                string[] data = readline.Split(',');
                string lineID = data[0];
                string line = data[1];

                // lineID �� Empty�� �ƴ϶�� currentLineID �� ����
                if (lineID != string.Empty)
                {
                    currentLineID = lineID;

                    // ����Ʈ�� �����ؼ� line�� ���� 
                    List<string> lines = new List<string>();
                    lines.Add(line);

                    //��ųʸ��� lineID �� List�� ����
                    dialogueDictionary.Add(lineID, lines);
                }

                // ���� �ҷ��Դµ� lineID�� ����ִٸ�
                // currentLineID �� �����ߴ� lineID�� Key������ ��ųʸ��� ã�� line�� �߰�
                else
                {
                    dialogueDictionary[currentLineID].Add(line);
                }

                // ���� �� �а� while�� ���� ( while�� �� �� ���� ���� �����ϱ� ���� )
                readline = sr.ReadLine();

            }
        }

        public List<string> GetTextData(string lineID)  // lineID�� ���� List �ҷ����� �Լ�
        {
            return dialogueDictionary[lineID];
        }
    }
}