using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Item / Interactable")]
    public class InteractableItem : ItemData
    {
        public GameObject ObjectRef;        //  상호작용이 될 오브젝트
        public GameObject playerObjectRef;  // 플레이어 레퍼런스
                                            // 현재 상호작용 콜라이더와 충돌 중인지 여부를 알기 위함
    }
}
