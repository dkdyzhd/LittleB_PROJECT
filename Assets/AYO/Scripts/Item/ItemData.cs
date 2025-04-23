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
            Document = 0,       // 문서
            Consumable = 1,     // 소모
            Grace = 2,          // 신록
            Interactable = 3,   // 상호작용
            Combination = 4,    // 조합
        }

        public string itemID;
        public string itemName;
        public ItemType itemType;
        public Sprite itemIcon;
        public string inspectText;
        public int itemCount;

        [Header("Stacking")]
        public bool isStackable;       // 스택 가능 여부
        public int maxStackAmount;  // 최대 스택 가능

        [Header("Disposable")]
        public bool isDispoable;
    }
}
