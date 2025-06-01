using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using JetBrains.Annotations;
using System.Net;

namespace AYO
{
    public class InteractionItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private InvenManager invenManager;
        [SerializeField] private ItemData itemData;

        [Header("상호작용 아이템's 오브젝트")]
        [SerializeField] private GameObject objectRef;
        [Header("아이템 기능")]
        [SerializeField] private UltEvent onUse;
        [Header("아이템 사용 조건")]
        [SerializeField] private CondiotionList conditions; // 아이템 사용 조건 체크

        public ItemData ItemData => itemData;
        public UltEvent OnUse => onUse;

        public void OnInteract()
        {
            invenManager.AddItem(this); //this 로 바꾸기 & this에서 itemData를 가져와서 Additem 코드 변경
            gameObject.SetActive(false);
        }

        public void TestItemUSe()
        {
            if (conditions == null) onUse.Invoke();

            if (conditions.IsSatisfiedCondiotions())
            {
                onUse.Invoke();
            }
            else
            {
                return;
            }
        }

        //public void Use(PlayerController player)   // 아이템마다 Use 의 내용이 다 다름 > 스크립트 따로 파기 or 내부에서 없으면 그냥 넘어가도록
        //{                   // AddItem 에서 나 자신을 넘길 것이기 때문에 Use 내부에서 검사만하고 넘어가도록 구현 예정
        //    if (onUse != null)
        //    {
        //        if (objectRef != null && player.IsObjectNear(objectRef))
        //        {
        //            onUse.Invoke();
        //        }
        //    }
        //}

    }
}
