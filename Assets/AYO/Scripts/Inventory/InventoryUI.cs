using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private List<SlotUI> slots;

        public void RefreshUI(List<SlotData> slotDataList)
        {
            int i = 0;
            for (; i < slotDataList.Count && i < slots.Count; i++)
            {
                //SlotData slotData = slotDataList[i];
                slots[i].Item = slotDataList[i].GetItemData();
                slots[i].Count = slotDataList[i].GetItemCount();
            }
            for (; i < slots.Count; i++)
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
