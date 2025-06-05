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

        [SerializeField] private AudioSource audioSource;   // ����
        [SerializeField] private AudioClip itemAcquire;

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

        //Ư�� �������� ���� ���� �� by ���� 250528
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

        public void AddItem(InteractionItem item)
        {
            // ���ð����� �������̶��
            if(item.ItemData.isStackable)
            {
                // GetExistItemStackable() ȣ�� -> �̹� �����ϴ� ������ ������ã��
                //index : ������ ��ġ / result : �ش� ������ SlotData
                int index = GetExistItemStackable(item.ItemData, out SlotData result);
                if(result != null && index >= 0)
                {
                    // ���� �������� ������  ���� ������ count++ 
                    result.SetSlotItemCount(1);
                    Debug.Log($" ���� ������ {item.ItemData.itemName} ���� ����: {result.GetItemCount()}");
                    
                    // To do :  �κ��丮 count���� ����
                    invenUI.SetSlotUICount(index, result.GetItemCount());
                    return;
                }
            }

            // �� ���� ã�� & ���ο�ĭ�� ������ �� �� �� �ε��� ����
            int i = 0;
            for (i = 0;i < slotDataList.Count;i++)      //������ ����Ʈ�� ��ȣ�� �˷���
            {
                if (slotDataList[i].GetItemData() == null) 
                {
                    //slotDataList[i] = slotdata;       //*** 20250528_ �����ؾߵ�
                    break;
                }
            }

            // �� ������ �߰�
            SlotData slotdata = new SlotData();
            slotdata.SetSlotItem(item);     // *** 20250528_ �׳� item ���� ������ ��ü�� �Ѱ��ֱ�
            slotdata.SetSlotItemCount(1);
            slotDataList.Add(slotdata);
            Debug.Log($" �� ������ �߰�: {item.ItemData.itemName}, ����: 1");

            audioSource.PlayOneShot(itemAcquire);   // ����
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
                    slotDataList.Remove(result);    // => �� �����۵��� �� ������
                    //slotDataList[slotIndex].SetSlotItemData(null);    // => �� ĭ�� �״�� ��������
                }
            }

            invenUI.RefreshUI(slotDataList);
        }
        // ������ �������� �� �������� onUse ����
        public void SelectSlot(int index)
        {
            if (index < 0 || index >= slotDataList.Count)
            {
                Debug.LogWarning($"[SelectSlot] �߸��� �ε��� ����: {index}");
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
