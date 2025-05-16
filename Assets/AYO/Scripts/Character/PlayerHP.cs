using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class PlayerHP : MonoBehaviour
    {
        [SerializeField] private int playerHP;

        public void TakeDamage(int life)
        {
            playerHP -= life;
            Debug.Log($"ÇöÀç HP : " + playerHP);
        }
    }
}
