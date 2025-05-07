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

        [SerializeField] private GameObject player;
        private Animator anim;

        private Vector3 scale;

        // Patrol 에 필요한 변수들
        private Vector3 basePos;    // ENPC의 기본 위치
        private Vector3 targetPos;  // 목표 좌표 저장
        private Vector3 dirVec;     // 이동할 방향 벡터
        private float moveDurTime;  // 이동하는데 걸리는 시간
        private float addTime;      // 출발하고나서 시간
        private float waitTime;     // 대기시간
        private bool moveOnOff;     // 움직임 제한 변수
        private int i;          // 몇번 째 웨이포인트를 타겟으로 할지
        private int beforei;        // 전에 다녀온 웨이포인트 인덱스를 저장(다음 인덱스의 증가/감소를 정하기 위함)

        // Combat 에 필요한 변수들
        private Vector3 playerDir;      // 플레이어쪽 방향 벡터 > 필요한가?
        private float distance;         // 플레이어와 enpc 사이 거리 
        private float canAttackDis;     // 공격가능 거리

        private void Start()
        {
            anim = GetComponent<Animator>();
            waitTime = Random.Range(2, 3);
            scale = transform.localScale;
        }
        private void Update()
        {
            PatrolMove();
        }
        public void SetNextWayPointIndex()
        {
            if(i == 0)
            {
                beforei = i;
                i++;
            }
            else if(i >= wayPoint.Length - 1 || i < beforei)
            {
                beforei = i;
                i--;
            }
            else
            {
                beforei = i;
                i++;
            }
        }
        public void PatrolMove()
        {

            if (!moveOnOff) // 목표 지점에 도달 or 대기중상태
            {
                if(waitTime <= 0)   // 대기가 끝나면
                {
                    // 타겟 웨이포인트 저장
                    targetPos = wayPoint[i].position;
                    SetNextWayPointIndex();
                    
                    moveOnOff = true;       // 이동 활성화
                }
                else
                {
                    waitTime -= Time.deltaTime;
                    return;
                }
            }

            else    // patrol 이동
            {
                if(transform.position.x == targetPos.x)
                {
                    waitTime = Random.Range(2, 3);
                    moveOnOff = false;      // 이동 완료
                    anim.SetBool("Walk", false);
                }

                else
                {
                    // 목표 지점으로 이동
                    //dirVec = (targetPos - transform.position).normalized;
                    //transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                    anim.SetBool("Walk", true);
                    // 지금은 x축만 계산하면됨 >> Vector2로 계산하는건 필요 이상으로 계산이 들어가서 불필요
                    Vector2 ePos = transform.position;
                    ePos.x = Mathf.MoveTowards(transform.position.x, targetPos.x, moveSpeed * Time.deltaTime);
                    transform.position = ePos;

                    // 목표 지점을 바라보도록 
                    Vector2 dir = transform.right;
                    if(targetPos.x != transform.position.x) // 멈춰있을 때는 안 바꿈
                    {
                        dir.x = (targetPos.x < transform.position.x) ? -1f : 1f;
                        transform.right = dir;
                    }
                }
            }
        }

        public void CombatMode()
        {
            // 플레이어 위치 저장
            targetPos = player.transform.position;
            // 플레이어 방향
            playerDir = (transform.position - targetPos).normalized;
            // 플레이어와 거리
            distance = Vector3.Distance(player.transform.position, transform.position);

            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }

        public void ENPCAI()
        {

        }
    }
}
