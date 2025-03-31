using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class Speaking : MonoBehaviour
    {
        public string characterName;
        public string lineID;
        public List<string> lines;
        public Sprite sprite;

        public Speaking(string characterName, string lineID, List<string> lines, Sprite sprite)
        {
            this.characterName = characterName;
            this.lineID = lineID;
            this.lines = lines;
            this.sprite = sprite;
        }

        public void CurrentSpeaking(string characterName, string lineID, List<string> lines, Sprite sprite)
        {
            this.characterName = characterName;
            this.lineID = lineID;
            this.lines = lines;
            this.sprite = sprite;
        }

    }
}
