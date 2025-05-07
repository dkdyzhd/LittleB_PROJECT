using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private Animator anim;

        private Vector3 scale;

        // Patrol �� �ʿ��� ������
        private Vector3 basePos;    // ENPC�� �⺻ ��ġ
        private Vector3 targetPos;  // ��ǥ ��ǥ ����
        private Vector3 dirVec;     // �̵��� ���� ����
        private float moveDurTime;  // �̵��ϴµ� �ɸ��� �ð�
        private float addTime;      // ����ϰ��� �ð�
        private float waitTime;     // ���ð�
        private bool moveOnOff;     // ������ ���� ����
        private int i;          // ��� ° ��������Ʈ�� Ÿ������ ����
        private int beforei;        // ���� �ٳ�� ��������Ʈ �ε����� ����(���� �ε����� ����/���Ҹ� ���ϱ� ����)

        // Combat �� �ʿ��� ������
        private Vector3 playerDir;      // �÷��̾��� ���� ���� > �ʿ��Ѱ�?
        private float distance;         // �÷��̾�� enpc ���� �Ÿ� 
        private float canAttackDis;     // ���ݰ��� �Ÿ�

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

            if (!moveOnOff) // ��ǥ ������ ���� or ����߻���
            {
                if(waitTime <= 0)   // ��Ⱑ ������
                {
                    // Ÿ�� ��������Ʈ ����
                    targetPos = wayPoint[i].position;
                    SetNextWayPointIndex();
                    
                    moveOnOff = true;       // �̵� Ȱ��ȭ
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
                    moveOnOff = false;      // �̵� �Ϸ�
                    anim.SetBool("Walk", false);
                }

                else
                {
                    // ��ǥ �������� �̵�
                    //dirVec = (targetPos - transform.position).normalized;
                    //transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                    anim.SetBool("Walk", true);
                    // ������ x�ุ ����ϸ�� >> Vector2�� ����ϴ°� �ʿ� �̻����� ����� ���� ���ʿ�
                    Vector2 ePos = transform.position;
                    ePos.x = Mathf.MoveTowards(transform.position.x, targetPos.x, moveSpeed * Time.deltaTime);
                    transform.position = ePos;

                    // ��ǥ ������ �ٶ󺸵��� 
                    Vector2 dir = transform.right;
                    if(targetPos.x != transform.position.x) // �������� ���� �� �ٲ�
                    {
                        dir.x = (targetPos.x < transform.position.x) ? -1f : 1f;
                        transform.right = dir;
                    }
                }
            }
        }

        public void CombatMode()
        {
            // �÷��̾� ��ġ ����
            targetPos = player.transform.position;
            // �÷��̾� ����
            playerDir = (transform.position - targetPos).normalized;
            // �÷��̾�� �Ÿ�
            distance = Vector3.Distance(player.transform.position, transform.position);

            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }

        public void ENPCAI()
        {

        }
    }
}
