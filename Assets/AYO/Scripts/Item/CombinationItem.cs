using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Item / Combination")]
    public class CombinationItem : ItemData
    {
        public string combinationID;

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
