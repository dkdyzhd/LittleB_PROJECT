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

        private void Update()
        {
            // 움직이게하기
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 충돌하면 사라짐
            Destroy(gameObject);
        }
    }
}
