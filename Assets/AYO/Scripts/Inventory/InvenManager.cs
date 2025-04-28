using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class InvenManager : MonoBehaviour
    {
        private SlotData[] slotDataList;
        
        private InventoryUI invenUI;


        // ������ ������ ���� �������� ������ �ִٸ� �� �ε����� ��ȯ
        public int GetExistItemStackable(ItemData itemData, out SlotData resultData)    
        {
            for (int i = 0; i < slotDataList.Length; i++)
            {
                //if(slotDataList.GetSlotItemData(i) == itemData)
                //{
                //    resultData = slotDataList.GetSlotData(i);
                //    return i;
                //}

                SlotData currentSlot = slotDataList[i];
                if (currentSlot.GetItemData() == itemData)
                {
                    resultData = currentSlot;
                    return i;
                }
            }
            resultData = null;
            return -1;  // ������ -1 ��ȯ
        }

        public void AddItem(ItemData itemData)
        {
            // ���ð����� �������̶��
            if(itemData.isStackable)
            {
                // GetExistItemStackable() ȣ�� -> �̹� �����ϴ� ������ ������ã��
                //index : ������ ��ġ / result : �ش� ������ SlotData
                int index = GetExistItemStackable(itemData, out SlotData result);
                if(result != null && index >= 0)
                {
                    // ���� �������� ������  ���� ������ count++ 
                    result.SetSlotItemCount(1);
                    Debug.Log($" ���� ������ {itemData.itemName} ���� ����: {result.GetItemCount()}");
                    
                    // To do :  �κ��丮 count���� ����
                    invenUI.SetSlotUICount(index, result.GetItemCount());
                    return;
                }
            }

            // �� ���� ã�Ƽ� �� ������ �߰�
            for (int i = 0;i < slotDataList.Length;i++)
            {
                SlotData currentslot = slotDataList[i];
                if (currentslot.GetItemData() == null)
                {
                    currentslot.SetSlotItemData(itemData);

                    Debug.Log($" �� ������ �߰�: {itemData.itemName}, ����: 1");

                    // To do :  �κ��丮 Refresh ����
                    invenUI.RefreshUI(slotDataList); 
                    return;
                }
            }
        }
    }
}
