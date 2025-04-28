using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private SlotUI[] slots;

        public void RefreshUI(SlotData[] slotDataList)
        {
            int i = 0;
            for (; i < slotDataList.Length && i < slots.Length; i++)
            {
                SlotData slotData = slotDataList[i];
                slots[i].Item = slotData.GetItemData();
                slots[i].Count = slotData.GetItemCount();
            }
            for (; i < slots.Length; i++)
            {
                slots[i].Item = null;
            }
        }

        public void SetSlotUICount(int index, int count)
        {
            slots[index].Count = count;
        }
    }
}
