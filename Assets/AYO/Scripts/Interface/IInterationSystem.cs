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

            // currentInteractable�� ������ ���� FŰ ��ȣ�ۿ� ����
            if (currentInteractable != null && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F Ű �Է� ������");
                currentInteractable.OnInteract();
            }
        }

        void DetectInteractable()
        {
            Vector2 centerPosition = (Vector2)transform.position + offset;
            int count = Physics2D.OverlapCircleNonAlloc(centerPosition, interactionRange, hitColliders, interactLayer);

            // �÷��̾ ��ȣ�ۿ밡���� ������Ʈ �Ѱ��ֱ� _ 20250528
            player.SetCurrentInteractable(hitColliders);

            // ���� �����ӿ��� ������ ��ȣ�ۿ� ���� ������Ʈ�� ���� ���, null�� �ʱ�ȭ
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

                Debug.Log($"������ ������Ʈ: {hitCollider.name}");

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

            // ���� �ֽ� ����(currentInteractable) ������Ʈ�� �ٲ������ Ȯ��
            if (currentInteractable != nearestInteractable)
            {
                currentInteractable = nearestInteractable;

                if (nearestCollider != null)
                {
                    Debug.Log($"��ȣ�ۿ� ������ ������Ʈ: {nearestCollider.name}");
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
