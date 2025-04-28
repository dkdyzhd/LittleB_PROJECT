using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class SlotUI : MonoBehaviour
    {
        private Image itemImage;                        // ������ �̹���
        private TMPro.TextMeshProUGUI countText;        // ������ ����
        private ItemData currentItem;                   // �޾ƿ� ������ ����


        public ItemData item
        {
            get { return currentItem; }
            set
            {
                currentItem = value;        // ������ �޾ƿ���
                if (currentItem != null )
                {
                    itemImage.sprite = item.itemIcon;
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
