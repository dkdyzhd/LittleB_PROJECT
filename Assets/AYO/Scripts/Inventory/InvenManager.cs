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


        // ������ ������ ���� �������� ������ �ִٸ� �� �ε����� ��ȯ
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

            // �� ���� ã�� & ���ο�ĭ�� ������ �� �� �� �ε��� ����
            int i = 0;
            for (i = 0;i < slotDataList.Count;i++)      //������ ����Ʈ�� ��ȣ�� �˷���
            {
                //SlotData currentslot = slotDataList[i];
                if (slotDataList[i].GetItemData() == null) // (currentslot.GetItemData() == null
                {
                    break;
                }
            }

            // �� ������ �߰�
            SlotData slotdata = new SlotData();
            slotdata.SetSlotItemData(itemData);
            slotdata.SetSlotItemCount(1);
            slotDataList.Add(slotdata);
            Debug.Log($" �� ������ �߰�: {itemData.itemName}, ����: 1");

            //slotDataList[i].SetSlotItemData(itemData);
            //slotDataList[i].SetSlotItemCount(1);
            invenUI.RefreshUI(slotDataList);



        }
    }
}
