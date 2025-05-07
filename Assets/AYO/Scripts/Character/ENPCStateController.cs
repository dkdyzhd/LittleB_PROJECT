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
        private Vector3 playerDir;
        private float distance;

        private void Start()
        {
            anim = GetComponent<Animator>();
            waitTime = Random.Range(2, 3);
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
                    // ���µ� �ɸ� �ð� = �Ÿ� / �ӵ�
                    moveDurTime = Vector3.Distance(transform.position, targetPos) / moveSpeed;
                    addTime = 0.0f;
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
                addTime += Time.deltaTime;
                if(addTime >= moveDurTime)
                {
                    waitTime = Random.Range(2, 3);
                    moveOnOff = false;      // �̵� �Ϸ�
                    anim.SetBool("Walk", false);
                }
                else
                {
                    anim.SetBool("Walk", true);
                    // ��ǥ �������� �̵�
                    dirVec = (targetPos - transform.position).normalized;
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                    // ��ǥ ������ �ٶ󺸵���
                    Vector3 scale = transform.localScale;
                    // Ÿ���� x��ǥ ����
                    scale.x = (targetPos.x < transform.position.x) ? 1 : -1;    // ĳ����Sprite�� ������ ���� �־ �¿� ���� �ٲ�
                    transform.localScale = scale;
                }
            }
        }

        public void CombatMode()
        {
            // �÷��̾� ����
            //playerDir = (transform.position - player.transform.position).normalized;
            // �÷��̾�� �Ÿ�
            distance = Vector3.Distance(player.transform.position, transform.position);
            // �÷��̾� ��ġ ����
            targetPos = player.transform.position;

            dirVec = playerDir.normalized;
            transform.position = Vector2.MoveTowards(transform.position, dirVec, moveSpeed * Time.deltaTime);
        }

        public void ENPCAI()
        {

        }
    }
}
