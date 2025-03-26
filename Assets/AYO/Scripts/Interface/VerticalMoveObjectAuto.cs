using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class VerticalMoveObjectAuto : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 2f;
        [SerializeField]
        private float downOffset = 5f;
        [SerializeField]
        private float upOffset = 5f;

        private Rigidbody2D rb;
        private float downBound;
        private float upBound;
        private int direction = -1;        // ���� �̵� ���� (-1: ����, +1: ������)
        private bool isActivated = true;  // Lever ON/OFF ����

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();


            float initY = rb.position.y;
            downBound = initY - downOffset;
            upBound = initY + upOffset;
        }

        private void Update()
        {
            if (isActivated)
            {

                float currentY = rb.position.y;


                if (currentY <= downBound && direction < 0)
                {
                    direction = 1;
                }

                else if (currentY >= upBound && direction > 0)
                {
                    direction = -1;
                }

                rb.velocity = new Vector2(rb.velocity.x, direction * moveSpeed);
            }
            else
            {

                rb.velocity = Vector2.zero;
            }
        }



    }
}