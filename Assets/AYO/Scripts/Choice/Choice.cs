using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace AYO
{
    [System.Serializable]   //MonoBehaviour �� �Ⱦ��� ��ſ� ����Ƽ�󿡼� �����ֱ� ���� ����ȭ
    public class Choice 
    {

        [SerializeField] private Condition condition;
        [SerializeField] private string choiceID;
        [SerializeField] private UltEvent nextEvent;

        public bool ChoiceCondition()
        {
            if (condition.Invoke())   // �� ������ bool �� Ŭ������ ��ӹ޴µ� �ȵǴ���? 
                return true;            // >> condition�� �Լ��� �����ϴ� ����-> �Լ� �������� ��������

            return false;
        }
        public string GetChoiceID()
        {
            return choiceID;
        }

        public void InvokeNextEvent()
        {
            nextEvent.Invoke();
        }

        public UltEvent NextEvent()
        {
            return nextEvent;
        }
    }
}
