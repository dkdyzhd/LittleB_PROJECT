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

        [Header("��ȣ�ۿ� ������'s ������Ʈ")]
        [SerializeField] private GameObject objectRef;
        [Header("������ ���")]
        [SerializeField] private UltEvent onUse;
        [Header("������ ��� ����")]
        [SerializeField] private CondiotionList conditions; // ������ ��� ���� üũ

        public ItemData ItemData => itemData;
        public UltEvent OnUse => onUse;

        public void OnInteract()
        {
            invenManager.AddItem(this); //this �� �ٲٱ� & this���� itemData�� �����ͼ� Additem �ڵ� ����
            gameObject.SetActive(false);
        }

        public void TestItemUSe()
        {
            if (conditions == null) onUse.Invoke();

            if (conditions.IsSatisfiedCondiotions())
            {
                onUse.Invoke();
            }
            else
            {
                return;
            }
        }

        //public void Use(PlayerController player)   // �����۸��� Use �� ������ �� �ٸ� > ��ũ��Ʈ ���� �ı� or ���ο��� ������ �׳� �Ѿ����
        //{                   // AddItem ���� �� �ڽ��� �ѱ� ���̱� ������ Use ���ο��� �˻縸�ϰ� �Ѿ���� ���� ����
        //    if (onUse != null)
        //    {
        //        if (objectRef != null && player.IsObjectNear(objectRef))
        //        {
        //            onUse.Invoke();
        //        }
        //    }
        //}

    }
}
