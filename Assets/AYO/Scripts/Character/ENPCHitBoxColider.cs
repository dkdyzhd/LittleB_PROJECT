using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class ENPCHitBoxColider : MonoBehaviour
    {
        [SerializeField] private HyenaSkill skill;
        private PlayerController playerCtrler;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                playerCtrler = collision.GetComponent<PlayerController>();

                //To do : 스킬
                //skill.Attack(); 흠...... 스킬 재활용가능하도록
                Debug.Log("공격당함!");
            }
        }
    }
}
