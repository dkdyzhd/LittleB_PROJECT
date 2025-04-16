using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class ChoiceArray : MonoBehaviour
    {

        [SerializeField] private CharacterData characterData;
        [SerializeField] private Choice[] choiceArray;

        public Choice GetChoice(int i)
        {
            return choiceArray[i];
        }

        public int GetChoiceCount()
        {
            return choiceArray.Length;
        }

        public CharacterData GetCharacterData()
        {
            return characterData;
        }
    }
}
