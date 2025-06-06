using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

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

        [SerializeField] private ENPCHitBoxCollider collider;

        [Header("웨이포인트")]
        [SerializeField] private Transform[] wayPoint;

        [SerializeField] private GameObject player;
        private ENPCAIState enpcAIState;
        private Animator anim;
        private PlayerController playerController;
        private SpriteRenderer sr;
        private bool isDead = false;
        //투명해지는 속도
        private float colorSpeed = 1f;

        //Sound 관련 변수들 by 휘익 250528
        [Header("사운드 설정")]
        /*[SerializeField] private string attackSFXName = "Enemy_Attack"; // SFXLibrary에 등록된 공격 사운드 이름
        [SerializeField] private float attackSFXVolumeScale = 1.0f;
        [SerializeField] private bool useAttackSFX = true; // 공격 사운드 사용 여부*/
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip attackSound;

        // Patrol 에 필요한 변수들
        private Vector3 basePos;    // ENPC의 기본 위치
        private Vector3 targetPos;  // 목표 좌표 저장
        private Vector3 dirVec;     // 이동할 방향 벡터
        private float moveDurTime;  // 이동하는데 걸리는 시간
        private float addTime;      // 출발하고나서 시간
        private float waitTime;     // 대기시간
        private bool moveOn;     // 움직임 제한 변수
        private int i;          // 몇번 째 웨이포인트를 타겟으로 할지
        private int beforei;        // 전에 다녀온 웨이포인트 인덱스를 저장(다음 인덱스의 증가/감소를 정하기 위함)

        // Combat 에 필요한 변수들
        private Vector3 playerDir;      // 플레이어쪽 방향 벡터 > 필요한가?
        private float distance;         // 플레이어와 enpc 사이 거리 
        [Header("공격 가능 거리")]
        [SerializeField] private float canAttackDis;     // 공격가능 거리
        private bool aggroOn;
        private bool isAttacking;
        private Vector3 lastSeenPlayerPos;

        private void Start()
        {
            playerController = player.GetComponent<PlayerController>();
            anim = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            waitTime = Random.Range(2, 3);

            audioSource = GetComponent<AudioSource>();  //휘익

            SetAIState(ENPCAIState.AI_Patrol);
            isAttacking = false;
        }
        private void Update()
        {
            if(playerController.IsDead)
            {
                SetAIState(ENPCAIState.AI_Patrol);
            }
            if (!isDead)
            {
                ENPCAI();
            }

            else
            {
                //서서히 투명해지도록
                sr.color = Vector4.Lerp(sr.color, new Vector4(1, 1, 1, 0), Time.deltaTime * colorSpeed);

                //많이 투명해지면
                if (sr.color.a <= 0.1f)
                {
                    //게임오브젝트 비활성화
                    this.gameObject.SetActive(false);
                }
            }
        }
        public void SetAIState(ENPCAIState state)
        {
            if (enpcAIState == state) return; // 동일 상태로 전환 방지
            enpcAIState = state;
            Debug.Log("현재 상태: " + enpcAIState);
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

        public void ChangeDir(Vector3 targetP)
        {
            // 목표 지점을 바라보도록 
            Vector2 dir = transform.right;
            if (targetP.x != transform.position.x) // 멈춰있을 때는 안 바꿈
            {
                dir.x = (targetP.x < transform.position.x) ? -1f : 1f;
                transform.right = dir;
            }
        }

        public void MoveTo(Vector3 targetP)
        {
            if (isAttacking) { return; }

            anim.SetBool("Walk", true);

            // 지금은 x축만 계산하면됨 >> Vector2로 계산하는건 필요 이상으로 계산이 들어가서 불필요
            Vector2 ePos = transform.position;
            ePos.x = Mathf.MoveTowards(transform.position.x, targetP.x, moveSpeed * Time.deltaTime);
            transform.position = ePos;

            // 목표 지점을 바라보도록 
            ChangeDir(targetP);
        }

        public void Patrol()
        {
            if (!moveOn) // 목표 지점에 도달 or 대기중상태
            {
                if(waitTime <= 0)   // 대기가 끝나면
                {
                    // 타겟 웨이포인트 저장
                    targetPos = wayPoint[i].position;
                    SetNextWayPointIndex();
                    
                    moveOn = true;       // 이동 활성화
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
                    moveOn = false;      // 이동 완료
                    anim.SetBool("Walk", false);
                }

                else
                {
                    // 목표 지점으로 이동
                    MoveTo(targetPos);
                }
            }
        }

        public void ToggleAggro(bool on)
        {
            aggroOn = on; 
        }

        public void SetLastSeenPlayerPos(Vector3 playerPos)
        {
            lastSeenPlayerPos = playerPos;
        }

        public void AggroTrace()
        {
            MoveTo(player.transform.position);
        }

        public void NormalTrace()
        {
            
            MoveTo(lastSeenPlayerPos);
            
            if (transform.position.x == lastSeenPlayerPos.x)
            {
                waitTime = 1.0f;        // 구별을 주기 위해 잠시 대기 시간 걸기
                moveOn = false;      // 이동 완료
                anim.SetBool("Walk", false);
                SetAIState(ENPCAIState.AI_Patrol);
                return;
            }
        }

        public void Attack()
        {
            if(!isAttacking)
            {
                moveOn = false;
                anim.SetBool("Walk", false);
                anim.SetTrigger("Trigger_Attack");

                isAttacking = true;
                StartCoroutine(DelayAfterAttack());
            }
        }

        public void Combat()
        {
            //To do :  AI _Combat용 함수 구현
        }

        IEnumerator DelayAfterAttack()
        {
            yield return new WaitForSeconds(1.5f);
            moveOn = true;
            isAttacking = false;
        }

        public void ENPCAI()
        {
            switch(enpcAIState)
            {

                case ENPCAIState.AI_Patrol:
                    Patrol();
                    break;

                case ENPCAIState.AI_Combat:
                    // 플레이어와 거리
                    float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
                    //Debug.Log("플레이어 거리: " + distance);

                    if (aggroOn)
                    {
                        if (distance <= canAttackDis)
                        {
                            Attack();
                        }
                        else
                        {
                            AggroTrace();
                        }
                    }
                    else
                    {
                        NormalTrace();
                    }
                    break;
            }
        }

        public void GetDamage(int damage)
        {
            if (isDead) return;
            maxHp -= damage;
            Debug.Log($"ENPC Hp : " + maxHp);
            if (maxHp <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            isDead = true;
            anim.SetTrigger("HyenaDead");
        }

        public void PlayAttackSFX() // by 휘익 250528
        {
            audioSource.PlayOneShot(attackSound);
        }

        private void ActivateHitBox()   // Animator
        {
            collider.StartAttack();
        }

        private void DeActivateHitBox() //Animator
        {
            collider.FinishAttack();
        }
    }
}
