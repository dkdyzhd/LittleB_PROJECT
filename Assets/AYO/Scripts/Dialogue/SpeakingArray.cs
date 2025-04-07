using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace AYO
{
    public class SpeakingArray : MonoBehaviour
    {
        [Header("Speaking 배열")]
        [SerializeField] private Speaking[] array;
        [SerializeField] private UnityEvent nextEvent;

        public Speaking GetSpeaking(int i)
        {
            return array[i];
        }

        public int GetArrayLength()
        {
            return array.Length;
        }

        public void InvokeNextEvent()
        {
            nextEvent.Invoke();
        }
    }
}


class ChoiceTableLoader
{
    [SerializeField] private TextAsset dialogueTable;

    private Dictionary<string, List<string>> dialogueDictionary = new Dictionary<string, List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        StringReader sr = new StringReader(dialogueTable.text);
        string readline = sr.ReadLine();
        string currentLineID = string.Empty;

        while (!string.IsNullOrEmpty(readline))
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

            // 다음 줄 읽고 while문 종료 ( while문 돌 때 다음 줄을 저장하기 위함 )
            readline = sr.ReadLine();

        }
    }

    public List<string> GetDialogueData(string lineID)  // lineID를 통해 List 불러오는 함수
    {
        return dialogueDictionary[lineID];
    }
}