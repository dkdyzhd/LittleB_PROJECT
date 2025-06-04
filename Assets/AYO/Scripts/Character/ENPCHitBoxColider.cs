using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class ENPCHitBoxColider : MonoBehaviour
    {
        [SerializeField] private KnifeSkill skill;
        [SerializeField] private Collider2D hitCollider;
        [SerializeField] private ContactFilter2D contactFilter;
        private PlayerController playerCtrler;
        private bool isAttacking;
        private Vector3 contactVec;

        private List<Collider2D> detectedColliders = new List<Collider2D>(); // 감지된 콜라이더들

        private void FixedUpdate()
        {
            if(isAttacking)
            {
                int count = hitCollider.OverlapCollider(contactFilter, detectedColliders);
                for(int i = 0; i < count; i++)
                {
                    
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player" && !isAttacking)
            {
                isAttacking = true;
                playerCtrler = collision.GetComponent<PlayerController>();

                contactVec = (playerCtrler.transform.position - skill.transform.position).normalized ;

                skill.Attack(playerCtrler, contactVec);
                Debug.Log("공격당함!");
            }
        }

    }
}
