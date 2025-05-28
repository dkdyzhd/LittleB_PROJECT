using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace AYO
{
    [System.Serializable]
    public class SlotData 
    {
        private int count;
        private ItemData slotItemData;
        private UltEvent useItem;

        public void SetItemUse(UltEvent use)
        {
            use = useItem;
        }

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
