using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;           // ���ư��� �ӵ�
        [SerializeField] private float rigWeight;       // �߷� ����ġ
        [SerializeField] private float fireRange;       // ���� �Ÿ�..?

        private void Update()
        {
            // �����̰��ϱ�
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // �浹�ϸ� �����
            Destroy(gameObject);
        }
    }
}
