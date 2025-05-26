using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using JetBrains.Annotations;

namespace AYO
{
    public class InteractionItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] private InvenManager invenManager;
        [SerializeField] private UltEvent onUse;

        public ItemData ItemData => itemData;

        public void OnInteract()
        {
            invenManager.AddItem(itemData); //this �� �ٲٱ� & this���� itemData�� �����ͼ� Additem �ڵ� ����
            gameObject.SetActive(false);
        }

        public ItemData GetItemData()
        {
            return itemData;
        }

        public void Use()   // �����۸��� Use �� ������ �� �ٸ� > ��ũ��Ʈ ���� �ı� or ���ο��� ������ �׳� �Ѿ����
        {                   // AddItem ���� �� �ڽ��� �ѱ� ���̱� ������ Use ���ο��� �˻縸�ϰ� �Ѿ���� ���� ����
            if (onUse != null)
                onUse.Invoke();
        }

    }
}
