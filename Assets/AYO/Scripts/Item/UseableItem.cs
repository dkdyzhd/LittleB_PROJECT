using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Item / Useable")]
    public class UseableItem : ItemData
    {
        [Header("Expendable")]  // 사용 시 소모 여부
        public bool isExpendable;

        public string itemEffectID;
        public float itemCooldown;

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
