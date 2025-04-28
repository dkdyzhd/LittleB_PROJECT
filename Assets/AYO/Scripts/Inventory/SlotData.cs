using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [System.Serializable]
    public class SlotData 
    {
        private ItemData slotItemData;
        private int count;

        public void SetSlotItemCount(int i)
        {
            count += i;
        }

        public void SetSlotItemData(ItemData itemData)
        {
            slotItemData = itemData;
        }

        public int GetItemCount()
        {
            return count;
        }

        public ItemData GetItemData()
        {
            return slotItemData;
        }
    }
}
