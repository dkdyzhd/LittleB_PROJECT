using System.Collections;
using UnityEngine;
using Cinemachine; // Cinemachine 네임스페이스

namespace AYO
{
    public class CameraZoomAction : DirectorAction
    {
        [Header("Zoom Target")]
        [Tooltip("줌 효과를 적용할 CinemachineVirtualCamera입니다.")]
        [SerializeField] private CinemachineVirtualCamera targetVCam;

        [Header("Zoom Settings")]
        [Tooltip("목표 Field of View (Perspective 카메라용)")]
        [SerializeField] private float targetFOV = 60f;
        [Tooltip("목표 Orthographic Size (Orthographic 카메라용)")]
        [SerializeField] private float targetOrthoSize = 5f;

        [Tooltip("줌 효과에 걸리는 시간(초)입니다.")]
        [SerializeField] private float duration = 1.0f;

        [Tooltip("줌 변화에 적용할 애니메이션 커브입니다. (선택적)")]
        [SerializeField] private AnimationCurve zoomCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // 기본 EaseInOut

        private bool _isPerspective;
        private float _initialValue;
        private float _targetValue;

        void Awake()
        {
            if (targetVCam == null)
            {
                Debug.LogError($"CameraZoomAction ({gameObject.name}): TargetVCam이 할당되지 않았습니다!", this);
                enabled = false; return;
            }
        }

        public override IEnumerator Execute()
        {
            if (!enabled || targetVCam == null)
            {
                Debug.LogError($"CameraZoomAction ({gameObject.name}): 실행 요건 미충족.", this);
                yield break;
            }

            _isPerspective = !targetVCam.m_Lens.Orthographic;

            if (_isPerspective)
            {
                _initialValue = targetVCam.m_Lens.FieldOfView;
                _targetValue = targetFOV;
                Debug.Log($"CameraZoomAction ({gameObject.name}): VCam '{targetVCam.Name}' FOV를 {_initialValue}에서 {targetFOV}로 {duration}초 동안 변경 시작.", this);
            }
            else
            {
                _initialValue = targetVCam.m_Lens.OrthographicSize;
                _targetValue = targetOrthoSize;
                Debug.Log($"CameraZoomAction ({gameObject.name}): VCam '{targetVCam.Name}' OrthoSize를 {_initialValue}에서 {targetOrthoSize}로 {duration}초 동안 변경 시작.", this);
            }

            if (duration <= 0) // 즉시 변경
            {
                if (_isPerspective) targetVCam.m_Lens.FieldOfView = _targetValue;
                else targetVCam.m_Lens.OrthographicSize = _targetValue;
                yield break;
            }

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                float curveValue = (zoomCurve != null && zoomCurve.keys.Length > 0) ? zoomCurve.Evaluate(t) : t;

                float currentValue = Mathf.LerpUnclamped(_initialValue, _targetValue, curveValue);

                if (_isPerspective)
                {
                    targetVCam.m_Lens.FieldOfView = currentValue;
                }
                else
                {
                    targetVCam.m_Lens.OrthographicSize = currentValue;
                }
                yield return null;
            }

            // 최종 값 정확히 설정
            if (_isPerspective) targetVCam.m_Lens.FieldOfView = _targetValue;
            else targetVCam.m_Lens.OrthographicSize = _targetValue;

            Debug.Log($"CameraZoomAction ({gameObject.name}): VCam '{targetVCam.Name}' 줌 변경 완료.", this);

            CompleteAction(); // 액션 완료 알림
        }
    }
}
