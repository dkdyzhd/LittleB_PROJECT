using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Item / Useable")]
    public class UseableItem : ItemData
    {
        [Header("Expendable")]  // ��� �� �Ҹ� ����
        public bool isExpendable;

        public string itemEffectID;
        public float itemCooldown;

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
