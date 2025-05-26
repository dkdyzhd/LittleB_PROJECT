using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Item / Document")]
    public class DocumentItem : ItemData
    {
        public Sprite imageToDisplay;

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
