using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class KnockBackCollider : MonoBehaviour
    {
        private PlayerController playerCtrler;
        private Vector3 contactVec;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.DrawLine(collision.contacts[0].point, collision.contacts[0].point + collision.contacts[0].normal);
            if (collision.gameObject.CompareTag("Player"))
            {
                playerCtrler = collision.gameObject.GetComponent<PlayerController>();
                contactVec = collision.contacts[0].normal;
                //³Ë¹é
                playerCtrler.KnockBack(contactVec);
            }
        }
    }
}
