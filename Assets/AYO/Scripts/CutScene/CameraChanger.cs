using System.Collections;
using UnityEngine;
// using Cinemachine; // CinemachineBrain을 직접 사용하지 않으므로 네임스페이스 제거 가능

namespace AYO
{
    public class CameraChanger : DirectorAction
    {
        [Header("Animator Settings")]
        [Tooltip("시네머신 카메라들을 제어하는 Animator 컴포넌트입니다.")]
        [SerializeField] private Animator cameraAnimator;

        [Tooltip("전환할 Animator 상태의 이름입니다.")]
        [SerializeField] private string targetStateName;

        [Header("Timing Settings")]
        [Tooltip("이 액션이 완료될 때까지 기다릴 시간(초)입니다. 카메라 전환 애니메이션 및 블렌드에 충분한 시간을 설정하세요.")]
        [SerializeField] private float waitDuration = 1.0f; // 기본 1초 대기

        private int _targetStateHash;

        void Awake()
        {
            if (cameraAnimator == null)
            {
                // 이 컴포넌트가 카메라 제어 Animator와 같은 GameObject에 있다면 GetComponent 시도
                cameraAnimator = GetComponent<Animator>();
            }

            if (cameraAnimator == null)
            {
                Debug.LogError($"CameraChanger ({gameObject.name}): Camera Animator가 할당되지 않았습니다! 이 액션은 작동하지 않습니다.", this);
                enabled = false; // 필수 컴포넌트 없이는 액션 비활성화
                return;
            }

            if (string.IsNullOrEmpty(targetStateName))
            {
                Debug.LogError($"CameraChanger ({gameObject.name}): Target State Name이 설정되지 않았습니다! 이 액션은 작동하지 않습니다.", this);
                enabled = false; // 필수 설정 없이는 액션 비활성화
                return;
            }

            _targetStateHash = Animator.StringToHash(targetStateName);
        }

        public override IEnumerator Execute()
        {
            // Awake에서 enabled = false 처리되었으면 실행되지 않음
            if (!enabled)
            {
                Debug.LogWarning($"CameraChanger ({gameObject.name}): 비활성화된 액션입니다. 실행되지 않습니다.", this);
                yield break;
            }

            Debug.Log($"CameraChanger ({gameObject.name}): Animator 상태를 '{targetStateName}'(으)로 변경합니다. {waitDuration}초 동안 대기합니다.", this);

            // Animator 상태 변경
            cameraAnimator.Play(_targetStateHash);

            // 지정된 시간(waitDuration)만큼 대기
            if (waitDuration > 0)
            {
                yield return new WaitForSeconds(waitDuration);
            }
            else
            {
                // waitDuration이 0 이하면 한 프레임만 대기 (즉시 완료와 유사)
                yield return null;
            }

            Debug.Log($"CameraChanger ({gameObject.name}): '{targetStateName}'로의 카메라 전환 액션 완료 (지정 시간 경과).", this);

            CompleteAction();
        }
    }
}