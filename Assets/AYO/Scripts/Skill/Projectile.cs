using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;           // 날아가는 속도
        [SerializeField] private float rigWeight;       // 중력 가중치
        [SerializeField] private float fireRange;       // 사정 거리..?

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
                // damage 입히기
                enpc = collision.gameObject.GetComponent<ENPCStateController>();
                enpc.GetDamage(damage);
            }
            // 충돌하면 사라짐
            Destroy(gameObject);
        }
    }
}
