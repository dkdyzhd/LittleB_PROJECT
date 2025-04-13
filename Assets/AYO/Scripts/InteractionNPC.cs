using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class InteractionNPC : MonoBehaviour, IInteractable
    {
        [SerializeField] private CharacterData characterData;
        [SerializeField] private DialManager dialManager;
        [SerializeField] private Speaking npcSpeaking;
        [SerializeField] private ChoiceManager choiceManager;
        [SerializeField] private ChoiceArray npcChoiceArray;

        public void OnInteract()
        {
            // ��ȭ ����
            dialManager.StartDialogue();
            // npc�� ������ �ִ� ������ ����
            choiceManager.GetChoiceArray(npcChoiceArray);
        }

        public CharacterData GetCharacterData()
        {
            return characterData;
        }

    }
}
