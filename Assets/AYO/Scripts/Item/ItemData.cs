using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class ItemData : ScriptableObject
    {
        public enum ItemType
        {
            Document = 0,       // ����
            Consumable = 1,     // �Ҹ�
            Grace = 2,          // �ŷ�
            Interactable = 3,   // ��ȣ�ۿ�
            Combination = 4,    // ����
        }

        public string itemID;
        public string itemName;
        public ItemType itemType;
        public Sprite itemIcon;
        public string inspectText;
        public int itemCount;

        [Header("Stacking")]
        public bool isStackable;       // ���� ���� ����
        public int maxStackAmount;  // �ִ� ���� ����

        [Header("Disposable")]
        public bool isDispoable;
    }
}
