using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace AYO
{
    public class SpeakingArray : MonoBehaviour
    {
        [Header("Speaking �迭")]
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

    public List<string> GetDialogueData(string lineID)  // lineID�� ���� List �ҷ����� �Լ�
    {
        return dialogueDictionary[lineID];
    }
}