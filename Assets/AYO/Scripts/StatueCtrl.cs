using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class StatueCtrl : MonoBehaviour
    {
        [SerializeField] private GameObject[] wings = new GameObject[4];       // 4개의 날개 오브젝트
        [SerializeField] private GameObject statueOn;      // 완성 시 켜질 statue_on 오브젝트

        private int wingCount = 0;

        // 아이템 사용 시 호출되는 메서드
        public void ActivateWing(int wingNumber)
        {
            if (!wings[wingNumber].activeSelf)
            {
                wings[wingNumber].SetActive(true);
                wingCount++;

                // 모든 날개가 켜졌는지 확인
                if (wingCount == wings.Length)
                {
                    statueOn.SetActive(true);
                }
            }

            //if (wingCount < wings.Length)
            //{
            //    wings[wingCount].SetActive(true);
            //    wingCount++;

            //    // 모든 날개가 활성화되었을 경우 statue_on 활성화
            //    if (wingCount == wings.Length)
            //    {
            //        statueOn.SetActive(true);
            //    }
            //}
        }
    }
}
