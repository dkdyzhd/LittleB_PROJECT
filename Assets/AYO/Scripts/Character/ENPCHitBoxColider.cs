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

                //To do : ��ų
                //skill.Attack(); ��...... ��ų ��Ȱ�밡���ϵ���
                Debug.Log("���ݴ���!");
            }
        }
    }
}
