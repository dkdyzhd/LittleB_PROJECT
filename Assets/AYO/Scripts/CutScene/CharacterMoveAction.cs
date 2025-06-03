using UnityEngine;
using System.Collections;


namespace AYO
{
    public class CharacterMoveAction : DirectorAction
    {
        [Header("Target Character")]
        [Tooltip("이동시킬 캐릭터의 GameObject입니다.")]
        [SerializeField] private GameObject targetCharacter;

        [Header("Movement Settings")]
        [Tooltip("캐릭터가 이동할 목표 위치입니다. 월드 좌표계를 사용합니다.")]
        [SerializeField] private Vector3 targetPosition;

        [Tooltip("이동에 걸리는 시간(초)입니다. 0보다 커야 합니다.")]
        [SerializeField] private float duration = 1.0f;

        [Tooltip("이동 시작 시 캐릭터가 목표 지점을 바라보도록 방향을 전환할지 여부입니다.")]
        [SerializeField] private bool faceTargetDirection = true;

        [Tooltip("캐릭터가 Rigidbody2D를 사용하여 물리 기반으로 이동해야 하는 경우 체크합니다 (선택 사항).")]
        [SerializeField] private bool useRigidbody2D = false;

        [Tooltip("useRigidbody2D가 체크된 경우, Rigidbody2D에 적용할 이동 속도입니다. duration은 무시됩니다.")]
        [SerializeField] private float rigidbodySpeed = 5f; // duration 대신 속도 기반 이동

        private Transform _characterTransform;
        private Rigidbody2D _rb2D;
        private Vector3 _startPosition;
        private Vector3 _originalScale; // 방향 전환을 위한 원래 스케일 저장

        void Awake()
        {
            if (targetCharacter == null)
            {
                Debug.LogError($"CharacterMoveAction ({gameObject.name}): Target Character가 할당되지 않았습니다!", this);
                enabled = false; // 실행 불가
                return;
            }

            _characterTransform = targetCharacter.transform;
            _originalScale = _characterTransform.localScale; // 초기 스케일 저장

            if (useRigidbody2D)
            {
                _rb2D = targetCharacter.GetComponent<Rigidbody2D>();
                if (_rb2D == null)
                {
                    Debug.LogError($"CharacterMoveAction ({gameObject.name}): useRigidbody2D가 체크되었으나, Target Character '{targetCharacter.name}'에 Rigidbody2D가 없습니다! Transform 기반 이동으로 대체합니다.", targetCharacter);
                    useRigidbody2D = false; // Rigidbody2D 없으면 fallback
                }
                else if (_rb2D.bodyType == RigidbodyType2D.Static)
                {
                    Debug.LogWarning($"CharacterMoveAction ({gameObject.name}): Target Character '{targetCharacter.name}'의 Rigidbody2D 타입이 Static입니다. 물리 이동이 작동하지 않을 수 있습니다.", targetCharacter);
                }
            }

            if (!useRigidbody2D && duration <= 0)
            {
                Debug.LogWarning($"CharacterMoveAction ({gameObject.name}): Duration이 0 이하로 설정되었습니다. 캐릭터가 즉시 목표 위치로 이동합니다. (Transform 기반 이동 시)", this);
                // duration = 0.01f; // 아주 작은 값으로 설정하여 에러 방지 가능
            }
        }

        public override IEnumerator Execute()
        {
            if (!enabled || targetCharacter == null)
            {
                Debug.LogWarning($"CharacterMoveAction ({gameObject.name}): 비활성화되었거나 Target Character가 없어 실행되지 않습니다.", this);
                CompleteAction(); // 즉시 완료 처리하여 시퀀서가 멈추지 않도록 함
                yield break;
            }

            _startPosition = _characterTransform.position;
            Vector3 moveDirection = (targetPosition - _startPosition).normalized;

            // 방향 전환 로직
            if (faceTargetDirection && moveDirection.x != 0) // 수평 이동이 있을 때만 방향 전환
            {
                float currentScaleX = _characterTransform.localScale.x;
                float targetScaleXSign = Mathf.Sign(moveDirection.x);

                // 현재 방향과 목표 방향이 다를 경우에만 스케일 변경
                if (Mathf.Sign(currentScaleX) != targetScaleXSign)
                {
                    _characterTransform.localScale = new Vector3(Mathf.Abs(_originalScale.x) * targetScaleXSign, _originalScale.y, _originalScale.z);
                    Debug.Log($"CharacterMoveAction ({gameObject.name}): '{targetCharacter.name}' 방향 전환 (x scale to {targetScaleXSign}).");
                }
            }

            Debug.Log($"CharacterMoveAction ({gameObject.name}): '{targetCharacter.name}'을(를) {_startPosition}에서 {targetPosition}으로 이동 시작.");

            if (useRigidbody2D && _rb2D != null)
            {
                // Rigidbody2D를 사용한 이동 (속도 기반)
                // 이 방식은 duration 대신 rigidbodySpeed를 사용합니다.
                // 목표 지점에 도달했는지 여부를 계속 확인해야 합니다.
                // Kinematic Rigidbody에 velocity를 직접 설정하거나, Dynamic Rigidbody에 AddForce/velocity 사용
                if (_rb2D.bodyType == RigidbodyType2D.Kinematic)
                {
                    // Kinematic의 경우, FixedUpdate에서 MovePosition을 사용하거나, 직접 속도 설정
                    // 여기서는 간단히 velocity를 사용합니다.
                    _rb2D.velocity = moveDirection * rigidbodySpeed;
                    float sqrDistanceToTarget = (targetPosition - _characterTransform.position).sqrMagnitude;
                    float previousSqrDist = sqrDistanceToTarget;

                    // 목표 지점에 거의 도달하거나 지나쳤는지 확인
                    while (sqrDistanceToTarget > 0.01f) // 매우 작은 값으로 근접 확인
                    {
                        // 만약 목표를 지나쳤다면 (거리가 다시 늘어나기 시작) 멈춤
                        // 이는 직선 이동에서만 유효할 수 있으며, 복잡한 경로에서는 다른 방식 필요
                        if (Vector3.Dot((targetPosition - _characterTransform.position).normalized, moveDirection) < 0.5f) // 방향이 크게 틀어졌거나 지나침
                        {
                            Debug.Log($"CharacterMoveAction ({gameObject.name}): '{targetCharacter.name}'이(가) 목표 지점을 지나친 것으로 판단되어 정지.");
                            break;
                        }

                        sqrDistanceToTarget = (targetPosition - _characterTransform.position).sqrMagnitude;
                        // 무한 루프 방지 또는 이전 거리보다 늘어났는지 체크 (오버슈팅 시)
                        if (sqrDistanceToTarget > previousSqrDist && previousSqrDist < 0.5f) { // 이전 프레임에 가까웠는데 멀어졌다면
                            Debug.Log($"CharacterMoveAction ({gameObject.name}): '{targetCharacter.name}'이(가) 목표 지점을 오버슈팅한 것으로 판단되어 정지.");
                            break;
                        }
                        previousSqrDist = sqrDistanceToTarget;

                        yield return null; // 다음 프레임까지 대기
                    }
                    _rb2D.velocity = Vector2.zero; // 목표 도달 또는 지나침 후 정지
                    _characterTransform.position = targetPosition; // 정확한 위치로 보정
                }
                else // Dynamic Rigidbody (AddForce 또는 velocity 직접 제어)
                {
                    // Dynamic의 경우 AddForce를 사용하면 가속도가 붙으므로, 여기서는 velocity를 직접 제어
                    _rb2D.velocity = moveDirection * rigidbodySpeed;
                    // Dynamic의 경우 도착 판정이 더 복잡할 수 있음. 위와 유사한 로직 사용.
                    float sqrDistanceToTarget = (targetPosition - _characterTransform.position).sqrMagnitude;
                    while (sqrDistanceToTarget > 0.01f && Vector3.Dot((targetPosition - _characterTransform.position).normalized, moveDirection) > 0.5f)
                    {
                        sqrDistanceToTarget = (targetPosition - _characterTransform.position).sqrMagnitude;
                        yield return null;
                    }
                    _rb2D.velocity = Vector2.zero;
                    _characterTransform.position = targetPosition;
                }
            }
            else // Transform을 사용한 이동 (시간 기반)
            {
                if (duration <= 0) // 즉시 이동
                {
                    _characterTransform.position = targetPosition;
                }
                else
                {
                    float elapsedTime = 0f;
                    while (elapsedTime < duration)
                    {
                        elapsedTime += Time.deltaTime;
                        float t = Mathf.Clamp01(elapsedTime / duration); // 0과 1 사이 값으로 제한
                        _characterTransform.position = Vector3.Lerp(_startPosition, targetPosition, t);
                        yield return null; // 다음 프레임까지 대기
                    }
                    // 이동 완료 후 정확한 위치로 설정
                    _characterTransform.position = targetPosition;
                }
            }

            Debug.Log($"CharacterMoveAction ({gameObject.name}): '{targetCharacter.name}' 이동 완료. 최종 위치: {_characterTransform.position}");
            CompleteAction(); // 액션 완료 알림
        }
    }

} // 네임스페이스 끝