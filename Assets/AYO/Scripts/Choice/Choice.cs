using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace AYO
{
    [System.Serializable]   //MonoBehaviour 를 안쓰는 대신에 유니티상에서 보여주기 위한 직렬화
    public class Choice 
    {

        [SerializeField] private Condition condition;
        [SerializeField] private string choiceID;
        [SerializeField] private UltEvent nextEvent;

        public bool ChoiceCondition()
        {
            if (condition.Invoke())   // 왜 리턴이 bool 인 클래스를 상속받는데 안되는지? 
                return true;            // >> condition은 함수를 저장하는 공간-> 함수 실행결과로 따져야함

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
