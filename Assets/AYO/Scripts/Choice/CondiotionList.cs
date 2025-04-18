using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    [System.Serializable]
    public class CondiotionList
    {
        [SerializeField] private Condition[] conditions;

        // 조건들이 만족하는지 확인
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
