using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace AYO
{
    public enum ENPCAIState
    {
        AI_Patrol,  // ����(�Ϲ�) ���
        AI_Combat   // ���� ���
    }

    public class ENPCStateController : MonoBehaviour
    {
        [Header("ĳ���ͼӼ�")]
        [SerializeField] private int maxHp;
        [SerializeField] private float moveSpeed;

        [Header("��������Ʈ")]
        [SerializeField] private Transform[] wayPoint;

        [SerializeField] private GameObject player;
        private ENPCAIState enpcAIState;
        private Animator anim;

        // Patrol �� �ʿ��� ������
        private Vector3 basePos;    // ENPC�� �⺻ ��ġ
        private Vector3 targetPos;  // ��ǥ ��ǥ ����
        private Vector3 dirVec;     // �̵��� ���� ����
        private float moveDurTime;  // �̵��ϴµ� �ɸ��� �ð�
        private float addTime;      // ����ϰ��� �ð�
        private float waitTime;     // ���ð�
        private bool moveOn;     // ������ ���� ����
        private int i;          // ��� ° ��������Ʈ�� Ÿ������ ����
        private int beforei;        // ���� �ٳ�� ��������Ʈ �ε����� ����(���� �ε����� ����/���Ҹ� ���ϱ� ����)

        // Combat �� �ʿ��� ������
        private Vector3 playerDir;      // �÷��̾��� ���� ���� > �ʿ��Ѱ�?
        private float distance;         // �÷��̾�� enpc ���� �Ÿ� 
        [Header("���� ���� �Ÿ�")]
        [SerializeField] private float canAttackDis;     // ���ݰ��� �Ÿ�
        private bool aggroOn;
        private Vector3 lastSeenPlayerPos;

        private void Start()
        {
            anim = GetComponent<Animator>();
            waitTime = Random.Range(2, 3);
            
            SetAIState(ENPCAIState.AI_Patrol);
        }
        private void Update()
        {
            //PatrolMove();
            ENPCAI();
        }
        public void SetAIState(ENPCAIState state)
        {
            if (enpcAIState == state) return; // ���� ���·� ��ȯ ����
            enpcAIState = state;
            Debug.Log("���� ����: " + enpcAIState);
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

        public void ChangeDir()
        {
            // ��ǥ ������ �ٶ󺸵��� 
            Vector2 dir = transform.right;
            if (targetPos.x != transform.position.x) // �������� ���� �� �ٲ�
            {
                dir.x = (targetPos.x < transform.position.x) ? -1f : 1f;
                transform.right = dir;
            }
        }

        public void MoveTo(Vector3 targetP)
        {
            anim.SetBool("Walk", true);

            // ������ x�ุ ����ϸ�� >> Vector2�� ����ϴ°� �ʿ� �̻����� ����� ���� ���ʿ�
            Vector2 ePos = transform.position;
            ePos.x = Mathf.MoveTowards(transform.position.x, targetP.x, moveSpeed * Time.deltaTime);
            transform.position = ePos;

            // ��ǥ ������ �ٶ󺸵��� 
            ChangeDir();
        }

        public void Patrol()
        {
            if (!moveOn) // ��ǥ ������ ���� or ����߻���
            {
                if(waitTime <= 0)   // ��Ⱑ ������
                {
                    // Ÿ�� ��������Ʈ ����
                    targetPos = wayPoint[i].position;
                    SetNextWayPointIndex();
                    
                    moveOn = true;       // �̵� Ȱ��ȭ
                }
                else
                {
                    waitTime -= Time.deltaTime;
                    return;
                }
            }

            else    // patrol �̵�
            {
                if(transform.position.x == targetPos.x)
                {
                    waitTime = Random.Range(2, 3);
                    moveOn = false;      // �̵� �Ϸ�
                    anim.SetBool("Walk", false);
                }

                else
                {
                    // ��ǥ �������� �̵�
                    //dirVec = (targetPos - transform.position).normalized;
                    //transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

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
            //targetPos = player.transform.position;

            MoveTo(player.transform.position);
        }

        public void NormalTrace()
        {
            //targetPos = latestPlayerPos;
            
            MoveTo(lastSeenPlayerPos);

            if (transform.position.x == targetPos.x)
            {
                waitTime = 1.0f;        // ������ �ֱ� ���� ��� ��� �ð� �ɱ�
                moveOn = false;      // �̵� �Ϸ�
                anim.SetBool("Walk", false);
                SetAIState(ENPCAIState.AI_Patrol);
                return;
            }
        }

        public void Attack()
        {
            anim.SetBool("Walk", false);
            anim.SetTrigger("Trigger_Attack");
        }

        public void Combat()
        {
            //To do :  AI _Combat�� �Լ� ����
        }

        public void ENPCAI()
        {
            switch(enpcAIState)
            {

                case ENPCAIState.AI_Patrol:
                    Patrol();
                    break;

                case ENPCAIState.AI_Combat:
                    // �÷��̾�� �Ÿ�
                    float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
                    Debug.Log("�÷��̾� �Ÿ�: " + distance);
                    if (aggroOn)
                    {
                        if(distance <= canAttackDis)
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
    }
}
