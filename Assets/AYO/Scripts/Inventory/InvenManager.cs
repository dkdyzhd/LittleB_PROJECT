using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class InvenManager : MonoBehaviour
    {
        private SlotDataList slotDataList;

        public int GetExistItemStackable(ItemData itemData, out SlotData resultData)
        {
            for (int i = 0; i < slotDataList.GetSlotsLength(); i++)
            {
                //if(slotDataList.GetSlotItemData(i) == itemData)
                //{
                //    resultData = slotDataList.GetSlotData(i);
                //    return i;
                //}

                SlotData currentSlot = slotDataList.GetSlotData(i);
                if (currentSlot.GetItemData() == itemData)
                {
                    resultData = currentSlot;
                    return i;
                }
            }
            resultData = null;
            return -1;  // 없으면 -1 반환
        }

        public void AddItem(ItemData itemData)
        {
            // 스택가능한 아이템이라면
            if(itemData.isStackable)
            {
                // GetExistItemStackable() 호출 -> 이미 존재하는 동일한 아이템찾기
                //index : 슬롯의 위치 / result : 해당 슬롯의 SlotData
                int index = GetExistItemStackable(itemData, out SlotData result);
                if(result != null && index >= 0)
                {
                    // 기존 아이템이 있으면  슬롯 아이템 count++ 
                    result.SetSlotItemCount(1);
                    Debug.Log($" 기존 아이템 {itemData.itemName} 개수 증가: {result.GetItemCount()}");
                    
                    // To do :  인벤토리 count에도 적용
                    return;
                }
            }

            // 빈 슬롯 찾아서 새 아이템 추가
            for (int i = 0;i < slotDataList.GetSlotsLength();i++)
            {
                SlotData currentslot = slotDataList.GetSlotData(i);
                if (currentslot.GetItemData() == null)
                {
                    currentslot.SetSlotItemData(itemData);

                    Debug.Log($" 새 아이템 추가: {itemData.itemName}, 개수: 1");

                    // To do :  인벤토리 Refresh 구현
                    return;
                }
            }
        }
    }
}
