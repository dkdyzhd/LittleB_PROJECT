using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class IInterationSystem : MonoBehaviour
    {
        private IInteractable currentInteractable;

        [SerializeField]
        private float interactionRange = 3f;
        [SerializeField]
        private LayerMask interactLayer;

        private Vector2 offset = Vector2.zero;

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
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(centerPosition, interactionRange, interactLayer);

            // ���� �����ӿ��� ������ ��ȣ�ۿ� ���� ������Ʈ�� ���� ���, null�� �ʱ�ȭ
            if (hitColliders.Length == 0)
            {
                currentInteractable = null;
                return;
            }

            float minDistance = float.MaxValue;
            IInteractable nearestInteractable = null;
            Collider2D nearestCollider = null;

            foreach (Collider2D hitCollider in hitColliders)
            {
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
