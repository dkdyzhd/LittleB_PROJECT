using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [System.Serializable]
    public class CondiotionList
    {
        [SerializeField] private Condition[] conditions;

        // ���ǵ��� �����ϴ��� Ȯ��
        public bool IsSatisfiedCondiotions()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].Invoke())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
