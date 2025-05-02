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

        // Patrol �� �ʿ��� ������
        private Vector3 basePos;    // ENPC�� �⺻ ��ġ
        private Vector3 targetPos;  // ��ǥ ��ǥ
        private Vector3 dirVec;     // �̵��� ���� ����
        private float waitTime;     // ���ð�

    }
}
