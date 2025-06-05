using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float forcePower = 10f;      // ���ư��� �ӵ�
        [SerializeField] private float rigWeight;       // �߷� ����ġ
        [SerializeField] private float fireRange;       // ���� �Ÿ�..?

        private Rigidbody2D rig;
        private ENPCStateController enpc;
        private float shootDir;
        private int damage;

        private void Awake()
        {
            rig = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //transform.Translate( new Vector2(shootDir, 0) * speed * Time.deltaTime);
        }

        public void Fire(Vector2 dir)
        {
            rig.AddForce(dir.normalized * forcePower, ForceMode2D.Impulse);
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
