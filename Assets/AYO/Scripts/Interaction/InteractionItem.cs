using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using JetBrains.Annotations;

namespace AYO
{
    public abstract class InteractionItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private InvenManager invenManager;
        [SerializeField] private ItemData itemData;
        [SerializeField] private UltEvent onUse;

        public ItemData ItemData => itemData;
        public UltEvent OnUse => onUse;

        public void OnInteract()
        {
            invenManager.AddItem(this); //this �� �ٲٱ� & this���� itemData�� �����ͼ� Additem �ڵ� ����
            gameObject.SetActive(false);
        }

        public abstract void Use(PlayerController player);

        public void UseTest(PlayerController player)   // �����۸��� Use �� ������ �� �ٸ� > ��ũ��Ʈ ���� �ı� or ���ο��� ������ �׳� �Ѿ����
        {                   // AddItem ���� �� �ڽ��� �ѱ� ���̱� ������ Use ���ο��� �˻縸�ϰ� �Ѿ���� ���� ����
            if (onUse != null)
                onUse.Invoke();

            InteractableItem interactableItem = itemData as InteractableItem;
            if (player.IsObjectNear(interactableItem.ObjectRef))
            {
                onUse.Invoke();
            }
        }

    }
}
