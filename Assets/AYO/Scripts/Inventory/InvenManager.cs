using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AYO
{
    public class InvenManager : MonoBehaviour
    {
        private List<SlotData> slotDataList = new List<SlotData>();
        //private SlotData[] slotDataList = new SlotData[48];
        
        [SerializeField] private InventoryUI invenUI;


        // 스택이 가능한 같은 아이템을 가지고 있다면 그 인덱스를 반환
        public int GetExistItemStackable(ItemData itemData, out SlotData resultData)    
        {
            for (int i = 0; i < slotDataList.Count; i++)
            {
                //if(slotDataList.GetSlotItemData(i) == itemData)
                //{
                //    resultData = slotDataList.GetSlotData(i);
                //    return i;
                //}

                //SlotData currentSlot = slotDataList[i];
                if (slotDataList[i] != null && slotDataList[i].GetItemData() == itemData)
                {
                    resultData = slotDataList[i];
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
                    invenUI.SetSlotUICount(index, result.GetItemCount());
                    return;
                }
            }

            // 빈 슬롯 찾기 & 새로운칸을 만들어야 할 때 그 인덱스 저장
            int i = 0;
            for (i = 0;i < slotDataList.Count;i++)      //데이터 리스트의 번호만 알려줌
            {
                //SlotData currentslot = slotDataList[i];
                if (slotDataList[i].GetItemData() == null) // (currentslot.GetItemData() == null
                {
                    break;
                }
            }

            // 새 아이템 추가
            SlotData slotdata = new SlotData();
            slotdata.SetSlotItemData(itemData);
            slotdata.SetSlotItemCount(1);
            slotDataList.Add(slotdata);
            Debug.Log($" 새 아이템 추가: {itemData.itemName}, 개수: 1");

            //slotDataList[i].SetSlotItemData(itemData);
            //slotDataList[i].SetSlotItemCount(1);
            invenUI.RefreshUI(slotDataList);



        }
    }
}
