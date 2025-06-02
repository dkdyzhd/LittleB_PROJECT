using System.Collections;
using System.Collections.Generic;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField] private Image itemImage;                        // ������ �̹���
        [SerializeField] private TMPro.TextMeshProUGUI countText;        // ������ ����
        [SerializeField] private Button button;
        //private ItemData currentItem;                   // �޾ƿ� ������ ����
        private InteractionItem currentInteractionItem;
        private InvenManager invenManager;
        private int index;

        public void SetSlotIndex(int i, InvenManager manager)
        {
            index = i;
            invenManager = manager;

            button.onClick.RemoveAllListeners();  // �ߺ� ����
            button.onClick.AddListener(OnClickSlot);
        }
        public void OnClickSlot()
        {
            invenManager.SelectSlot(index);
        }

        public InteractionItem InterItem
        {
            get { return currentInteractionItem; }
            set
            {
                currentInteractionItem = value;     // ������ �޾ƿ���
                if (currentInteractionItem != null)
                {
                    itemImage.sprite = InterItem.ItemData.itemIcon;
                    itemImage.color =  new Color(1, 1, 1, 1);
                }
                else
                {
                    itemImage.sprite = null;
                    countText.text = string.Empty;
                    itemImage.color = new Color(1, 1, 1, 0);
                }
            }
        }
        public int Count
        {
            set
            {

                if (value > 1)
                {   // 1 ���� ũ�� ���� string���� ��ȯ
                    countText.text = value.ToString();
                }
                else
                {   // �ƴϸ� text ǥ�� ����
                    countText.text = string.Empty;
                }
            }
        }

        //public ItemData Item
        //{
        //    get { return currentItem; }
        //    set
        //    {
        //        currentItem = value;        // ������ �޾ƿ���
        //        if (currentItem != null )
        //        {
        //            itemImage.sprite = Item.itemIcon;
        //        }
        //        else
        //        {
        //            itemImage.sprite = null;
        //            countText.text = string.Empty;
        //        }
        //    }
        //}


    }
}
