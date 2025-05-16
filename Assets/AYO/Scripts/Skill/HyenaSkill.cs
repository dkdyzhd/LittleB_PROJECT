using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace AYO
{
    public class HyenaSkill : MonoBehaviour
    {
        [SerializeField] private GameObject hitBoxColider;
        [SerializeField] private PlayerHP playerHP;
        [SerializeField] private PlayerController playerCtrler;

        [SerializeField] private int damage;
        

        public void HitBoxOn()
        {
            hitBoxColider.gameObject.SetActive(true);
        }

        public void HitBoxOff()
        {
            hitBoxColider.gameObject.SetActive(false);
        }

        public void Attack(int damage)
        {
            playerHP.TakeDamage(damage);
            playerCtrler.KnockBack();
        }
    }
}
