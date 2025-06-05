using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float forcePower = 10f;      // 날아가는 속도
        [SerializeField] private float rigWeight;       // 중력 가중치
        [SerializeField] private float fireRange;       // 사정 거리..?

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
                // damage 입히기
                enpc = collision.gameObject.GetComponent<ENPCStateController>();
                enpc.GetDamage(damage);
            }
            // 충돌하면 사라짐
            Destroy(gameObject);
        }
    }
}
