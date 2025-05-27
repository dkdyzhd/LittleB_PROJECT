using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    [CreateAssetMenu (fileName ="ItemData", menuName = "Item / Item")]
    public class ItemData : ScriptableObject    // 각 타입의 아이템들이 상속받을 클래스
    {
        public string dataName;
        public string itemID;
        public string itemName;
        public Sprite itemIcon;
        public string inspectText;

        [Header("Stacking")]
        public bool isStackable;       // 스택 가능 여부
        public int maxStackAmount;  // 최대 스택 가능

        [Header("Disposable")]
        public bool isDispoable;

    }
}
