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

        //[SerializeField] private ChoiceManager choiceManager;
        //[SerializeField] private ChoiceArray npcChoiceArray;

        public void OnInteract()
        {
            // ù�λ� ����
            dialManager.FirstSpeakingArray(firstSpeakingArray);
            // ��ȭ ����
            dialManager.StartDialogue();
            // npc�� ������ �ִ� ������ ����
            //choiceManager.ShowChoiceArray(npcChoiceArray);
        }

        public CharacterData GetCharacterData()
        {
            return characterData;
        }

    }
}
