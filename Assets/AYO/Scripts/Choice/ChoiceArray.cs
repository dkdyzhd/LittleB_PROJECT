using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class ChoiceArray : MonoBehaviour
    {

        [SerializeField] private Choice[] choiceArray;
        [SerializeField] private CharacterData characterData;

        public Choice GetChoice(int i)
        {
            return choiceArray[i];
        }
    }
}
