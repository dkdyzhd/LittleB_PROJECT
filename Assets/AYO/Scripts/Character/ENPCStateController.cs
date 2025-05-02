using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public enum ENPCAIState
    {
        AI_Patrol,  // 순찰(일반) 모드
        AI_Combat   // 전투 모드
    }

    public class ENPCStateController : MonoBehaviour
    {
        [Header("캐릭터속성")]
        [SerializeField] private int maxHp;
        [SerializeField] private float moveSpeed;

        [Header("웨이포인트")]
        [SerializeField] private Transform[] wayPoint;

        // Patrol 에 필요한 변수들
        private Vector3 basePos;    // ENPC의 기본 위치
        private Vector3 targetPos;  // 목표 좌표
        private Vector3 dirVec;     // 이동할 방향 벡터
        private float waitTime;     // 대기시간

    }
}
