using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [CreateAssetMenu(fileName = "Character Data", menuName = "Character Data / ScriptableObject" )]
    public class CharacterData : ScriptableObject
    {
        public string characterName;
        public string lineID;
        public Sprite characterSprite;
    }
}
