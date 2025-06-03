using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class ImageDisplayAction : DirectorAction
    {
        // ... (기존 필드들은 동일하게 유지) ...
        [Header("Image Settings")]
        [Tooltip("화면에 이미지를 표시할 UI Image 컴포넌트입니다.")]
        [SerializeField] private Image targetImageUI;

        [Tooltip("표시할 Sprite 이미지입니다.")]
        [SerializeField] private Sprite imageToShow;

        [Header("Display Options")]
        [Tooltip("이미지를 표시할 시간(초)입니다. 0 또는 음수이면 HideImage()가 호출될 때까지 계속 표시됩니다.")]
        [SerializeField] private float displayDuration = 3.0f; // 설명 수정

        [Tooltip("이미지 표시 시작 시 페이드 인 효과를 사용할지 여부입니다.")]
        [SerializeField] private bool useFadeIn = false;
        [SerializeField] private float fadeInDuration = 0.5f;

        [Tooltip("이미지 숨김 시작 시 페이드 아웃 효과를 사용할지 여부입니다.")]
        [SerializeField] private bool useFadeOut = false; // displayDuration > 0 일 때와 HideImage() 호출 시 모두 적용 가능하도록 수정
        [SerializeField] private float fadeOutDuration = 0.5f;

        private Color _originalColor;
        private bool _isImageForceHidden = false; // HideImage() 호출 여부를 추적하는 플래그

        void Awake()
        {
            if (targetImageUI == null)
            {
                Debug.LogError($"ImageDisplayAction on {gameObject.name}: TargetImageUI가 할당되지 않았습니다!");
                enabled = false;
                return;
            }
            _originalColor = targetImageUI.color;
            targetImageUI.gameObject.SetActive(false);
            SetAlpha(0);
        }

        public override IEnumerator Execute()
        {
            if (targetImageUI == null || imageToShow == null)
            {
                Debug.LogError("ImageDisplayAction: TargetImageUI 또는 ImageToShow가 없습니다.");
                yield break;
            }

            _isImageForceHidden = false; // 액션 시작 시 플래그 초기화
            targetImageUI.sprite = imageToShow;
            targetImageUI.gameObject.SetActive(true);
            Debug.Log($"ImageDisplayAction: '{imageToShow.name}' 이미지를 표시합니다.");

            if (useFadeIn && fadeInDuration > 0)
            {
                yield return StartCoroutine(FadeImage(0f, _originalColor.a, fadeInDuration));
            }
            else
            {
                SetAlpha(_originalColor.a);
            }

            if (displayDuration > 0)
            {
                // 페이드 아웃 시간을 고려하여 실제 대기 시간 조정
                float waitTime = displayDuration;
                if (useFadeOut && fadeOutDuration > 0)
                {
                    // 실제 대기 시간은 전체 displayDuration에서 페이드아웃 시간을 제외한 만큼
                    // 또는 displayDuration 전체를 기다리고 페이드아웃을 별도로 처리
                    // 여기서는 displayDuration 만큼 기다리고, 그 후 페이드 아웃
                    waitTime = displayDuration;
                }

                // displayDuration 만큼 기다리되, 중간에 HideImage가 호출되면 즉시 중단
                float elapsedTime = 0f;
                while (elapsedTime < waitTime && !_isImageForceHidden)
                {
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                if (_isImageForceHidden) // HideImage()에 의해 중단된 경우
                {
                    // HideImage() 내부에서 페이드 아웃 및 비활성화 처리하므로 여기서는 추가 작업 없음
                    Debug.Log($"ImageDisplayAction: '{imageToShow.name}' 표시 중 HideImage() 호출로 중단됨.");
                }
                else // displayDuration 만큼 시간이 지난 경우
                {
                    if (useFadeOut && fadeOutDuration > 0)
                    {
                        yield return StartCoroutine(FadeImage(_originalColor.a, 0f, fadeOutDuration));
                    }
                    targetImageUI.gameObject.SetActive(false);
                    SetAlpha(0);
                    Debug.Log($"ImageDisplayAction: '{imageToShow.name}' 이미지를 displayDuration 후 숨깁니다.");
                }
            }
            else // displayDuration <= 0 (계속 표시)
            {
                Debug.Log($"ImageDisplayAction: '{imageToShow.name}' 이미지가 계속 표시됩니다. HideImage() 호출 대기 중...");
                // HideImage()가 호출될 때까지 (즉, _isImageForceHidden이 true가 될 때까지) 대기
                yield return new WaitUntil(() => _isImageForceHidden);
                Debug.Log($"ImageDisplayAction: '{imageToShow.name}' 이미지가 HideImage() 호출로 숨겨집니다 (지속 표시 모드).");
                // HideImage() 내부에서 페이드아웃 및 비활성화 처리됨
            }
            
            CompleteAction(); // 액션 완료 알림
        }

        private IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
        {
            // ... (기존 FadeImage 코드는 동일) ...
            float elapsedTime = 0f;
            Color color = targetImageUI.color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                SetAlpha(newAlpha);
                yield return null;
            }
            SetAlpha(endAlpha);
        }

        private void SetAlpha(float alpha)
        {
            // ... (기존 SetAlpha 코드는 동일) ...
            if (targetImageUI != null)
            {
                Color currentColor = targetImageUI.color;
                currentColor.a = alpha;
                targetImageUI.color = currentColor;
            }
        }

        // 이 메소드가 호출되면 Execute 코루틴의 대기가 풀리도록 함
        public void HideImage()
        {
            if (targetImageUI != null && targetImageUI.gameObject.activeSelf)
            {
                // 현재 Execute 코루틴이 이 ImageDisplayAction 인스턴스에서 실행 중이라면
                // _isImageForceHidden 플래그를 설정하여 Execute 코루틴의 WaitUntil 또는 루프를 종료시킴
                _isImageForceHidden = true;

                // 진행 중인 페이드 효과가 있다면 중지 (새로운 페이드 아웃을 위해)
                // StopAllCoroutines(); // StopAllCoroutines는 다른 코루틴도 중지시킬 수 있으므로 주의.
                                    // FadeImage 코루틴만 특정해서 중지하거나, 플래그로 제어하는 것이 더 안전.
                                    // 여기서는 HideImage()가 호출되면 Execute 코루틴이 자체적으로 종료되므로,
                                    // ForceHide 코루틴을 직접 호출.

                StartCoroutine(ProcessForceHide());
            }
        }

        private IEnumerator ProcessForceHide()
        {
            // _isImageForceHidden 이 true가 되면 Execute()의 대기가 풀림.
            // 실제 숨김 처리는 Execute()가 종료되면서 자연스럽게 되거나,
            // 여기서 강제로 수행할 수 있음.
            // displayDuration <= 0 일때는 Execute()가 HideImage()를 기다리고 있으므로,
            // HideImage()에서 직접 숨김 처리를 해줘야 함.

            if (useFadeOut && fadeOutDuration > 0 && targetImageUI.color.a > 0)
            {
                yield return StartCoroutine(FadeImage(targetImageUI.color.a, 0f, fadeOutDuration));
            }
            targetImageUI.gameObject.SetActive(false);
            SetAlpha(0);
            // Debug.Log($"ImageDisplayAction: '{targetImageUI.sprite?.name}' 이미지가 ProcessForceHide를 통해 숨겨졌습니다.");
            // 이 로그는 Execute 내부의 로그와 중복될 수 있음.
        }


        void OnValidate()
        {
            if (fadeInDuration < 0) fadeInDuration = 0;
            if (fadeOutDuration < 0) fadeOutDuration = 0;
        }
    }
}