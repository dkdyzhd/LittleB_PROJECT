using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class IInterationSystem : MonoBehaviour
    {
        private IInteractable currentInteractable;
        [SerializeField] private PlayerController player;

        [SerializeField]
        private float interactionRange = 3f;
        [SerializeField]
        private LayerMask interactLayer;

        private Vector2 offset = Vector2.zero;

        private Collider2D[] hitColliders = new Collider2D[5];

        void Update()
        {
            DetectInteractable();

            // currentInteractable이 존재할 때만 F키 상호작용 가능
            if (currentInteractable != null && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F 키 입력 감지됨");
                currentInteractable.OnInteract();
            }
        }

        void DetectInteractable()
        {
            Vector2 centerPosition = (Vector2)transform.position + offset;
            int count = Physics2D.OverlapCircleNonAlloc(centerPosition, interactionRange, hitColliders, interactLayer);

            // 플레이어가 상호작용가능한 오브젝트 넘겨주기 _ 20250528
            player.SetCurrentInteractable(hitColliders);

            // 현재 프레임에서 감지된 상호작용 가능 오브젝트가 없을 경우, null로 초기화
            if (count <= 0)
            {
                currentInteractable = null;
                return;
            }

            float minDistance = float.MaxValue;
            IInteractable nearestInteractable = null;
            Collider2D nearestCollider = null;

            for (int i = 0; i < count; i++)
            {
                Collider2D hitCollider = hitColliders[i];
                if (hitCollider == null) continue; 

                Debug.Log($"감지된 오브젝트: {hitCollider.name}");

                float distance = Vector2.Distance(centerPosition, hitCollider.transform.position);
                IInteractable interactable = hitCollider.GetComponent<IInteractable>();

                if (interactable != null && distance < minDistance)
                {
                    minDistance = distance;
                    nearestInteractable = interactable;
                    nearestCollider = hitCollider;

                    
                }
            }
            //foreach (Collider2D hitCollider in hitColliders)
            //{
               
            //}

            // 현재 주시 중인(currentInteractable) 오브젝트가 바뀌었는지 확인
            if (currentInteractable != nearestInteractable)
            {
                currentInteractable = nearestInteractable;

                if (nearestCollider != null)
                {
                    Debug.Log($"상호작용 가능한 오브젝트: {nearestCollider.name}");
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Vector2 centerPosition = (Vector2)transform.position + offset;
            Gizmos.DrawWireSphere(centerPosition, interactionRange);
        }
    }
}
