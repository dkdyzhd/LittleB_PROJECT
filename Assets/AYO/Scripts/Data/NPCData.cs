using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [CreateAssetMenu(fileName = "NPC Data", menuName = "NPC Data / ScriptableObject" )]
    public class NPCData : ScriptableObject
    {
        public string npcName;
        public string lineID;
        public Sprite npcSprite;
    }
}
