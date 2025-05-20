using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class ENPCHitBoxColider : MonoBehaviour
    {
        [SerializeField] private KnifeSkill skill;
        private PlayerController playerCtrler;
        private bool isDamageActive;
        private Vector3 contactVec;

        private void OnEnable()
        {
            isDamageActive = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player" && !isDamageActive)
            {
                isDamageActive = true;
                playerCtrler = collision.GetComponent<PlayerController>();
                contactVec = (playerCtrler.transform.position - skill.transform.position).normalized ;

                skill.Attack(playerCtrler, contactVec);
                Debug.Log("공격당함!");
            }
        }

    }
}
