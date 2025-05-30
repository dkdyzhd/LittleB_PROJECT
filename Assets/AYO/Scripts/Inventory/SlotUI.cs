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
        private ItemData currentItem;                   // �޾ƿ� ������ ����

        public int GetButtonIndex(UltEvent buttonEvent)
        {
            button.onClick.AddListener(() => buttonEvent.Invoke());
            return 1;
        }

        public ItemData Item
        {
            get { return currentItem; }
            set
            {
                currentItem = value;        // ������ �޾ƿ���
                if (currentItem != null )
                {
                    itemImage.sprite = Item.itemIcon;
                }
                else
                {
                    itemImage.sprite = null;
                    countText.text = string.Empty;
                }
            }
        }

        public int Count
        {
            set
            {

                if(value > 1)
                {   // 1 ���� ũ�� ���� string���� ��ȯ
                    countText.text = value.ToString();
                }
                else
                {   // �ƴϸ� text ǥ�� ����
                    countText.text = string.Empty;
                }
            }
        }
    }
}
