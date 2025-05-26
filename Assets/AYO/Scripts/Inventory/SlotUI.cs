using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField] private Image itemImage;                        // ������ �̹���
        [SerializeField] private TMPro.TextMeshProUGUI countText;        // ������ ����
        private ItemData currentItem;                   // �޾ƿ� ������ ����

        public void OnClick()
        {
            currentItem.Use();
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
