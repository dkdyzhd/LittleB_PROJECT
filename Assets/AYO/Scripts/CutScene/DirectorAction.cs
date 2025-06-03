using System.Collections;
using UnityEngine;
using UltEvents;
using System;

namespace AYO
{
    public abstract class DirectorAction : MonoBehaviour
    {
        // 액션 완료 시 호출될 이벤트 (CutsceneSequencer는 이 이벤트를 직접 사용하지 않음)
        // 하지만 다른 시스템에서 이 액션의 완료를 알아야 할 경우 유용할 수 있음
        //public event Action OnActionCompleted;
        [Tooltip("이 액션의 모든 로직이 완료되었을 때 호출될 이벤트입니다.")]
        [SerializeField] private UltEvent onExecutionCompletedEvent; // Or 'actionCompletedEvent', 'afterExecuteEvent'

        // 이 액션을 실행하는 메소드.
        // 이 코루틴은 자신의 모든 작업을 완료한 후, CompleteAction()을 호출하고 종료되어야 합니다.
        public abstract IEnumerator Execute();

        // 자식 클래스에서 액션의 모든 로직이 완료되었을 때 이 메소드를 호출합니다.
        // 이 메소드 호출 후 Execute() 코루틴은 더 이상 오래 걸리는 yield를 하지 않고 종료되어야 합니다.
        protected void CompleteAction()
        {
            Debug.Log($"Action '{this.GetType().Name}' on GameObject '{gameObject.name}' has logically completed.");
            onExecutionCompletedEvent?.Invoke(); // 필요한 경우 이벤트 호출
        }
    }
}