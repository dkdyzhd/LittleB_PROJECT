using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using JetBrains.Annotations;
using System.Net;

namespace AYO
{
    public class InteractionItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private InvenManager invenManager;
        [SerializeField] private ItemData itemData;
        [SerializeField] private UltEvent onUse;

        [Header("��ȣ�ۿ� ������'s ������Ʈ")]
        [SerializeField] private GameObject objectRef;

        public ItemData ItemData => itemData;
        public UltEvent OnUse => onUse;

        public void OnInteract()
        {
            invenManager.AddItem(this); //this �� �ٲٱ� & this���� itemData�� �����ͼ� Additem �ڵ� ����
            gameObject.SetActive(false);
        }

        public void Use(PlayerController player)   // �����۸��� Use �� ������ �� �ٸ� > ��ũ��Ʈ ���� �ı� or ���ο��� ������ �׳� �Ѿ����
        {                   // AddItem ���� �� �ڽ��� �ѱ� ���̱� ������ Use ���ο��� �˻縸�ϰ� �Ѿ���� ���� ����
            if (onUse != null)
            {
                if (objectRef != null && player.IsObjectNear(objectRef))
                {
                    onUse.Invoke();
                }
            }
        }

    }
}
