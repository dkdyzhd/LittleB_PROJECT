using AYO.InputInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AYO
{
    public class SlingShotSkill : MonoBehaviour
    {
        [Header("스킬")]
        [SerializeField] private Transform firePosition;
        [SerializeField] private GameObject rockPrefab;
        [SerializeField] private int skillDamage;
        //[SerializeField] private float shootDelay = 0.5f;

        private Projectile projectile;

        //private bool isShooting;
        //private float shootingTimer;

        private void Update()
        {
            //if (isShooting)
            //{
            //    shootingTimer -= Time.deltaTime;

            //    if (shootingTimer < 0)
            //    {
            //        isShooting = false;
            //    }
            //    return;
            //}
        }

        public void Shoot(float dir)
        {
            // 발사체 생성
            GameObject rockBullet = Instantiate(rockPrefab);
            Debug.Log("bullet 생성");
            projectile = rockBullet.GetComponent<Projectile>();
            Vector2 bulletDir = new Vector2 (dir, 0);
            projectile.Fire(bulletDir);
            projectile.SetDamage(skillDamage);

            rockBullet.transform.position = firePosition.transform.position;
        }
    }
}
