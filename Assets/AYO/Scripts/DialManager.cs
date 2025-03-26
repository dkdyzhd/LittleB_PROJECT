using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class DialManager : MonoBehaviour
    {
        private NPCData npcData;
        private string currentLineID;

        [SerializeField] private DialogueTableLoader tableLoader;
        [SerializeField] private Text dialogueLine;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        public void ShowDialogue()
        {
            // npcData = GetComponent<NPCData>();  >> 어떻게 가져올것인지?
            List<string> lines = tableLoader.GetDialogueData(currentLineID);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
