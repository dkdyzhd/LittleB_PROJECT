using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class SlotDataList : MonoBehaviour
    {
        [SerializeField] private SlotData[] slotDatas;

        private SlotData slot;

        public ItemData GetSlotItemData(int i)
        {
            return slotDatas[i].GetItemData();
        }
        public SlotData GetSlotData(int i)
        {
            return slotDatas[i];
        }

        public int GetSlotsLength()
        {
            return slotDatas.Length;
        }
    }
}
