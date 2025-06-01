using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private List<SlotUI> slots;
        [SerializeField] InvenManager invenManager;

        private void Awake()
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].SetSlotIndex(i, invenManager);
            }
        }

        public void RefreshUI(List<SlotData> slotDataList)
        {
            int i = 0;
            for (; i < slotDataList.Count && i < slots.Count; i++)
            {
                //SlotData slotData = slotDataList[i];
                slots[i].InterItem = slotDataList[i].GetItem(); // 20250531_¼öÁ¤

                //slots[i].Item = slotDataList[i].GetItemData();
                slots[i].Count = slotDataList[i].GetItemCount();
            }
            for (; i < slots.Count; i++)
            {
                slots[i].InterItem = null;
            }
        }
        
        public void SetSlotUICount(int index, int count)
        {
            slots[index].Count = count;
        }
    }
}
