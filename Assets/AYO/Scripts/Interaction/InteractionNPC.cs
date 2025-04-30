using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class InteractionNPC : MonoBehaviour, IInteractable
    {
        [SerializeField] private CharacterData characterData;
        [SerializeField] private DialManager dialManager;
        [SerializeField] private SpeakingArray firstSpeakingArray;


        public void OnInteract()
        {
            dialManager.ShowDialogue(firstSpeakingArray);
        }

        public CharacterData GetCharacterData()
        {
            return characterData;
        }

    }
}
