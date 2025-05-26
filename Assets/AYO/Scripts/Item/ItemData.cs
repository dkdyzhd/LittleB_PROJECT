using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    [CreateAssetMenu (fileName ="ItemData", menuName = "Item / Item")]
    public abstract class ItemData : ScriptableObject    // �� Ÿ���� �����۵��� ��ӹ��� Ŭ����
    {
        public string itemID;
        public string itemName;
        public Sprite itemIcon;
        public string inspectText;

        [Header("Stacking")]
        public bool isStackable;       // ���� ���� ����
        public int maxStackAmount;  // �ִ� ���� ����

        [Header("Disposable")]
        public bool isDispoable;

        public abstract void Use();
    }
}
