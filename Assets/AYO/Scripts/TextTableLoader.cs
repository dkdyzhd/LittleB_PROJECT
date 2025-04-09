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

        private Dictionary<string, string> choiceDic = new Dictionary<string, string>();

        // Start is called before the first frame update
        void Start()
        {
            LoadDialTable(dialogueTable);
            LoadChoiceTable(choiceTable);   
        }

        public void LoadDialTable(TextAsset textAsset)
        {
            StringReader sr = new StringReader(textAsset.text);
            string readline = sr.ReadLine();
            string currentLineID = string.Empty;

            while (!string.IsNullOrEmpty(readline))
            {
                string[] data = readline.Split(',');
                string id = data[0];
                string line = data[1];

                // lineID �� Empty�� �ƴ϶�� currentLineID �� ����
                if (id != string.Empty)
                {
                    currentLineID = id;

                    // ����Ʈ�� �����ؼ� line�� ���� 
                    List<string> lines = new List<string>();
                    lines.Add(line);

                    //��ųʸ��� lineID �� List�� ����
                    dialogueDictionary.Add(id, lines);
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

        public void LoadChoiceTable(TextAsset textAsset)
        {
            StringReader sr = new StringReader(textAsset.text);
            string readline = sr.ReadLine();

            while (!string.IsNullOrEmpty(readline))
            {
                string[] data = readline.Split(',');
                string id = data[0];
                string line = data[1];

                choiceDic.Add(id, line);

                // ���� �� �а� while�� ���� ( while�� �� �� ���� ���� �����ϱ� ���� )
                readline = sr.ReadLine();

            }
        }

        public List<string> GetTextData(string id)  // lineID�� ���� List �ҷ����� �Լ�
        {
            return dialogueDictionary[id];
        }

        public string GetChoiceData(string id)  // id�� ���� string�� �ҷ����� �Լ�
        {
            return choiceDic[id];
        }
    }
}