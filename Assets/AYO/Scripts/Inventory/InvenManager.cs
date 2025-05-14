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

        // �������� �ִ��� Ȯ��
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

        // ������ ������ ���� �������� ������ �ִٸ� �� �ε����� ��ȯ
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

        public void RemoveItem(ItemData item, int quantity)
        {
            int index = GetExistItemStackable(item, out SlotData result);   //result�� null�̸� -1�� ��ȯ
            int slotIndex = slotDataList.IndexOf(result);   // ���� ����Ʈ�� �ε����� �̾Ƴ��� �Լ���g= Ȱ���Ͽ� ����
            if (result != null && index >= 0)
            {
                result.SetSlotItemCount(-quantity);
                if(result.GetItemCount() <= 0)
                {   // �پ��� �����۵����Ͱ� �����Կ��� ����������
                    slotDataList[slotIndex].SetSlotItemData(null);
                }
            }

            invenUI.RefreshUI(slotDataList);
        }
    }
}
