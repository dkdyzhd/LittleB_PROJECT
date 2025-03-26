using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class DialogueTableLoader : MonoBehaviour
    {
        [SerializeField] private TextAsset dialogueTable;

        private Dictionary<string, List<string>> dialogueDictionary = new Dictionary<string, List<string>>();

        // Start is called before the first frame update
        void Start()
        {
            StringReader sr = new StringReader(dialogueTable.text);
            string readline = sr.ReadLine();
            string currentLineID = string.Empty;

            while(!string.IsNullOrEmpty(readline))
            {
                string[] data = readline.Split(',');
                string lineID = data[0];
                string line = data[1];

                // lineID 가 Empty가 아니라면 currentLineID 로 저장
                if (lineID != string.Empty)
                {
                    currentLineID = lineID;

                    // 리스트를 생성해서 line을 저장 
                    List<string> lines = new List<string>();
                    lines.Add(line);

                    //딕셔너리에 lineID 와 List를 저장
                    dialogueDictionary.Add(lineID, lines);
                }

                // 줄을 불러왔는데 lineID가 비어있다면
                // currentLineID 에 저장했던 lineID를 Key값으로 딕셔너리를 찾아 line을 추가
                else
                {
                    dialogueDictionary[currentLineID].Add(line);
                }

                readline = sr.ReadLine();
                // lineID 가 비어있지 않고 다를때


                // LN010100,으으... 바람 때문에 털이 엉망이 됐어.

                // ,바람산은 이름만 바람산이 아니구나…

                // LN010199,나는 아직 정식 연구원은 아니지만. 아는 만큼은 대답해줄게.
            }
        }

        public List<string> GetDialogueData(string lineID)
        {
            return dialogueDictionary[lineID];
        }
    }
}