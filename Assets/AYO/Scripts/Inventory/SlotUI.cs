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
        [SerializeField] private Image itemImage;                        // 아이템 이미지
        [SerializeField] private TMPro.TextMeshProUGUI countText;        // 아이템 갯수
        [SerializeField] private Button button;
        private ItemData currentItem;                   // 받아올 아이템 저장

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
                currentItem = value;        // 아이템 받아오기
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
                {   // 1 보다 크면 값을 string으로 변환
                    countText.text = value.ToString();
                }
                else
                {   // 아니면 text 표시 안함
                    countText.text = string.Empty;
                }
            }
        }
    }
}
