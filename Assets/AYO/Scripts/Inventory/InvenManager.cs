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

        [SerializeField] private PlayerController player;
        [SerializeField] private InventoryUI invenUI;
        [SerializeField] GameObject inventoryUI;
        [SerializeField] private PlayerInputEventManager pInputManager;

        [SerializeField] private AudioSource audioSource;   // 휘익
        [SerializeField] private AudioClip itemAcquire;

        // 아이템이 있는지 확인
        public bool HasItem(ItemData item, int quantity)
        {
            foreach (SlotData slotData in slotDataList)
            {
                if(slotData.GetItemData() == item)
                {
                    if(slotData.GetItemCount() >= quantity)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //특정 아이템이 있지 않을 때 by 휘익 250528
        public bool NotHasItem(ItemData item)
        {
            foreach (SlotData slotData in slotDataList)
            {
                if (slotData.GetItemData() == item)
                {
                    return false;
                }
            }
            return true;
        }

        // 스택이 가능한 같은 아이템을 가지고 있다면 그 인덱스를 반환
        public int GetExistItemStackable(ItemData itemData, out SlotData resultData)    
        {
            for (int i = 0; i < slotDataList.Count; i++)
            {
                if (slotDataList[i] != null && slotDataList[i].GetItemData() == itemData)
                {
                    resultData = slotDataList[i];
                    return i;
                }
            }
            resultData = null;
            return -1;  // 없으면 -1 반환
        }

        public void AddItem(InteractionItem item)
        {
            // 스택가능한 아이템이라면
            if(item.ItemData.isStackable)
            {
                // GetExistItemStackable() 호출 -> 이미 존재하는 동일한 아이템찾기
                //index : 슬롯의 위치 / result : 해당 슬롯의 SlotData
                int index = GetExistItemStackable(item.ItemData, out SlotData result);
                if(result != null && index >= 0)
                {
                    // 기존 아이템이 있으면  슬롯 아이템 count++ 
                    result.SetSlotItemCount(1);
                    Debug.Log($" 기존 아이템 {item.ItemData.itemName} 개수 증가: {result.GetItemCount()}");
                    
                    // To do :  인벤토리 count에도 적용
                    invenUI.SetSlotUICount(index, result.GetItemCount());
                    return;
                }
            }

            // 빈 슬롯 찾기 & 새로운칸을 만들어야 할 때 그 인덱스 저장
            int i = 0;
            for (i = 0;i < slotDataList.Count;i++)      //데이터 리스트의 번호만 알려줌
            {
                if (slotDataList[i].GetItemData() == null) 
                {
                    //slotDataList[i] = slotdata;       //*** 20250528_ 수정해야됨
                    break;
                }
            }

            // 새 아이템 추가
            SlotData slotdata = new SlotData();
            slotdata.SetSlotItem(item);     // *** 20250528_ 그냥 item 으로 아이템 자체를 넘겨주기
            slotdata.SetSlotItemCount(1);
            slotDataList.Add(slotdata);
            Debug.Log($" 새 아이템 추가: {item.ItemData.itemName}, 개수: 1");

            audioSource.PlayOneShot(itemAcquire);   // 휘익
            //slotDataList[i].SetSlotItemData(itemData);
            //slotDataList[i].SetSlotItemCount(1);
            invenUI.RefreshUI(slotDataList);
        }

        public void RemoveItem(ItemData item, int quantity)
        {
            int index = GetExistItemStackable(item, out SlotData result);   //result가 null이면 -1을 반환
            int slotIndex = slotDataList.IndexOf(result);   // 따로 리스트의 인덱스를 뽑아내는 함수를g= 활용하여 저장
            

            if (result != null && index >= 0)
            {
                result.SetSlotItemCount(-quantity);
                if(result.GetItemCount() <= 0)
                {   // 다쓰면 아이템데이터가 퀵슬롯에서 없어지도록
                    slotDataList.Remove(result);    // => 뒤 아이템들이 다 땡겨짐
                    //slotDataList[slotIndex].SetSlotItemData(null);    // => 빈 칸이 그대로 남아있음
                }
            }

            invenUI.RefreshUI(slotDataList);
        }
        // 슬롯을 선택했을 때 아이템의 onUse 실행
        public void SelectSlot(int index)
        {
            if (index < 0 || index >= slotDataList.Count)
            {
                Debug.LogWarning($"[SelectSlot] 잘못된 인덱스 접근: {index}");
                return;
            }
            slotDataList[index].GetItem().Use();
        }

        public void OnInventoryBag()
        {
            if (inventoryUI.activeInHierarchy)
            {
                inventoryUI.SetActive(false);
                pInputManager.LeftClickTarget = player;
            }
            else
            {
                inventoryUI.SetActive(true);
                pInputManager.LeftClickTarget = null;
            }
        }
    }
}
