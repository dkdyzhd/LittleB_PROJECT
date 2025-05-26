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

        private ENPCStateController enpc;
        private float shootDir;
        private int damage;


        private void Update()
        {
            transform.Translate( new Vector2(shootDir, 0) * speed * Time.deltaTime);
        }

        public void SetBulletDir(float bulletDir)
        {
            shootDir = bulletDir;
        }

        public void SetDamage(int skillDamage)
        {
            damage = skillDamage;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("ENPC"))
            {
                // damage ������
                enpc = collision.gameObject.GetComponent<ENPCStateController>();
                enpc.GetDamage(damage);
            }
            // �浹�ϸ� �����
            Destroy(gameObject);
        }
    }
}
