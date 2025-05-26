using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using JetBrains.Annotations;

namespace AYO
{
    public class InteractionItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] private InvenManager invenManager;
        [SerializeField] private UltEvent onUse;

        public ItemData ItemData => itemData;

        public void OnInteract()
        {
            invenManager.AddItem(itemData); //this 로 바꾸기 & this에서 itemData를 가져와서 Additem 코드 변경
            gameObject.SetActive(false);
        }

        public ItemData GetItemData()
        {
            return itemData;
        }

        public void Use()   // 아이템마다 Use 의 내용이 다 다름 > 스크립트 따로 파기 or 내부에서 없으면 그냥 넘어가도록
        {                   // AddItem 에서 나 자신을 넘길 것이기 때문에 Use 내부에서 검사만하고 넘어가도록 구현 예정
            if (onUse != null)
                onUse.Invoke();
        }

    }
}
