using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class KnifeSkill : MonoBehaviour
    {
        [SerializeField] private int skillDamage;
        
        public void Attack(PlayerController pc, Vector3 v)
        {
            pc.TakeDamage(skillDamage);
            pc.KnockBack(v);
        }
    }
}
