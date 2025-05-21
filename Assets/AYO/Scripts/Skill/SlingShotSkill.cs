using AYO.InputInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AYO
{
    public class SlingShotSkill : MonoBehaviour, ILeftMouseButtonTarget
    {
        [Header("입력매니저")]
        [SerializeField] private PlayerInputEventManager inputM;

        [Header("스킬")]
        [SerializeField] private Transform firePosition;
        [SerializeField] private GameObject rockPrefab;
        [SerializeField] private float skillDamage;
        [SerializeField] private float shootDelay = 0.5f;

        private bool isShooting;
        private float shootingTimer;

        private void Start()
        {
            inputM.LeftClickTarget = this;
        }

        private void Update()
        {
            if (isShooting)
            {
                shootingTimer -= Time.deltaTime;

                if (shootingTimer < 0)
                {
                    isShooting = false;
                }
                return;
            }
        }

        public void Shoot()
        {
            if (!isShooting)
            {
                isShooting = true;

                // 발사체 생성
                GameObject rockBullet = Instantiate(rockPrefab);
                rockBullet.transform.position = firePosition.transform.position;
                shootingTimer = shootDelay;
            }

        }

        public void OnLeftClick(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
