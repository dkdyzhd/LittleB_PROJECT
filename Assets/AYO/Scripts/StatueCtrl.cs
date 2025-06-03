using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class StatueCtrl : MonoBehaviour
    {
        [SerializeField] private GameObject[] wings = new GameObject[4];       // 4���� ���� ������Ʈ
        [SerializeField] private GameObject statueOn;      // �ϼ� �� ���� statue_on ������Ʈ

        private int wingCount = 0;

        // ������ ��� �� ȣ��Ǵ� �޼���
        public void ActivateWing(int wingNumber)
        {
            if (!wings[wingNumber].activeSelf)
            {
                wings[wingNumber].SetActive(true);
                wingCount++;

                // ��� ������ �������� Ȯ��
                if (wingCount == wings.Length)
                {
                    statueOn.SetActive(true);
                }
            }

            //if (wingCount < wings.Length)
            //{
            //    wings[wingCount].SetActive(true);
            //    wingCount++;

            //    // ��� ������ Ȱ��ȭ�Ǿ��� ��� statue_on Ȱ��ȭ
            //    if (wingCount == wings.Length)
            //    {
            //        statueOn.SetActive(true);
            //    }
            //}
        }
    }
}
