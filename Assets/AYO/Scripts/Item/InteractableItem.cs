using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Item / Interactable")]
    public class InteractableItem : ItemData
    {
        public GameObject ObjectRef;        //  ��ȣ�ۿ��� �� ������Ʈ
        public GameObject playerObjectRef;  // �÷��̾� ���۷���
                                            // ���� ��ȣ�ۿ� �ݶ��̴��� �浹 ������ ���θ� �˱� ����
    }
}
