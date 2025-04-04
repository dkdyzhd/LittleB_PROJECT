using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class InteractionNPC : MonoBehaviour, IInteractable
    {
        [SerializeField] private CharacterData characterData;
        [SerializeField] private DialManager dialManager;
        [SerializeField] private Speaking speaking;

        public void OnInteract()
        {
            //speaking.SetNPC(this);
            dialManager.StartDialogue();
        }

        public CharacterData GetCharacterData()
        {
            return characterData;
        }

    }
}
