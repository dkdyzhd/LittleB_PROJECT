using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class NPCInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueManager dialogueManager;

        [Header("��ȭ ���� LineID")]
        [SerializeField] private string startLineID;

        [Header("�÷��̾� ������(���) LineIDs")]
        [SerializeField] private List<string> choiceLineIDs = new List<string>(); // �������� ����Ʈ�� ����

        /// <summary>
        /// �÷��̾ F Ű ���� ������ ����
        /// </summary>
        public void OnInteract()
        {
            Debug.Log($"[NPCInteract] OnInteract() => StartConversation({startLineID}) on {gameObject.name}");
            if (dialogueManager != null && !string.IsNullOrEmpty(startLineID))
            {
                dialogueManager.StartConversation(startLineID, this);
            }
            else
            {
                Debug.LogWarning($"[NPCInteract] dialogueManager is null or startLineID is empty. startLineID={startLineID}");
            }
        }

        /// <summary>
        /// ���������� LineID�� ������ �� �ִ� �޼���
        /// </summary>
        public void SetChoiceLineIDs(List<string> newChoiceLineIDs)
        {
            choiceLineIDs = newChoiceLineIDs;
            Debug.Log($"[NPCInteract] SetChoiceLineIDs() called. New choices: {string.Join(", ", choiceLineIDs)}");
        }

        public Dialogue[] GetChoiceDialogues(DialogueData data)
        {
            Debug.Log($"[NPCInteract] GetChoiceDialogues() called on {gameObject.name}. choiceLineIDs.Count={choiceLineIDs.Count}");

            Dialogue[] arr = new Dialogue[choiceLineIDs.Count];

            for (int i = 0; i < choiceLineIDs.Count; i++)
            {
                arr[i] = data.GetDialogueByID(choiceLineIDs[i]);
                if (arr[i] == null)
                {
                    Debug.LogWarning($"[NPCInteract] choiceLineID={choiceLineIDs[i]} not found in DialogueData!");
                }
                else
                {
                    Debug.Log($"[NPCInteract] Found dialogue for {choiceLineIDs[i]} => nextLine={arr[i].nextLine}");
                }
            }
            return arr;
        }
    }
}
