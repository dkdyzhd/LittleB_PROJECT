using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class ENPCHitBoxCollider : MonoBehaviour
    {
        [SerializeField] private KnifeSkill skill;
        [Header("HitBox 콜라이더")]
        [SerializeField] private Collider2D hitCollider;
        [Header("ENPC가 공격중 감지할 콜라이더")]
        [SerializeField] private ContactFilter2D contactFilter;
        private bool isAttacking;
        private bool detectPlayer;
        private Vector3 contactVec;

        private List<Collider2D> detectedColliders = new List<Collider2D>(); // 감지된 콜라이더들 저장소

        private void FixedUpdate()
        {
            if(isAttacking && !detectPlayer)
            {
                int count = hitCollider.OverlapCollider(contactFilter, detectedColliders);
                for(int i = 0; i < count; i++)
                {
                    Collider2D collider = detectedColliders[i];
                    if (collider.CompareTag("Player"))
                    {
                        if(collider.TryGetComponent(out PlayerController playerCtrler))
                        {
                            detectPlayer = true;
                            contactVec = (playerCtrler.transform.position - skill.transform.position).normalized;
                            skill.Attack(playerCtrler, contactVec);
                        }
                    }
                }
            }
        }

        public void StartAttack()
        {
            isAttacking = true;
            hitCollider.enabled = true;
        }

        public void FinishAttack()
        {
            isAttacking = false;
            detectPlayer = false;
            hitCollider.enabled = false;
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if(collision.gameObject.tag == "Player" && !isAttacking)
        //    {
        //        isAttacking = true;
        //        playerCtrler = collision.GetComponent<PlayerController>();

        //        contactVec = (playerCtrler.transform.position - skill.transform.position).normalized ;

        //        skill.Attack(playerCtrler, contactVec);
        //        Debug.Log("공격당함!");
        //    }
        //}
    }
}
