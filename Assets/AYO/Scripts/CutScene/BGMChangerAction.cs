// BGMChangerAction.cs
using System.Collections;
using UnityEngine;

namespace AYO
{
    public class BGMChangerAction : DirectorAction
    {
        [Header("BGM Settings")]
        [Tooltip("BGMLibrary에 정의된 재생할 BGM의 고유 ID")]
        [SerializeField] private string bgmIDToPlay;

        [Tooltip("BGM 전환 시 사용할 페이드 시간(초). -1이면 SoundManager 또는 BGMEntry의 기본값 사용, 0이면 즉시 전환.")]
        [SerializeField] private float fadeDuration = -1f;

        public override IEnumerator Execute()
        {
            if (SoundManager.Instance == null)
            {
                Debug.LogError("BGMChangerAction: SoundManager 인스턴스를 찾을 수 없습니다!");
                yield break; // SoundManager 없이는 실행 불가
            }

            if (string.IsNullOrEmpty(bgmIDToPlay))
            {
                Debug.LogWarning("BGMChangerAction: 재생할 BGM ID가 지정되지 않았습니다.", this.gameObject);
                // BGM을 멈추고 싶다면 SoundManager.Instance.StopBGM(fadeDuration); 호출 가능
                // 여기서는 ID가 없으면 아무것도 안 하거나 경고만 출력
                yield break;
            }

            Debug.Log($"BGMChangerAction: Playing BGM with ID '{bgmIDToPlay}' with fade duration {fadeDuration}s.");
            SoundManager.Instance.PlayBGM(bgmIDToPlay, fadeDuration);

            // BGM 페이드가 완료될 때까지 기다릴지 여부는 선택사항입니다.
            // 일반적으로 BGM 변경은 다른 액션과 동시에 진행되어도 괜찮으므로,
            // 여기서는 즉시 다음 액션으로 넘어갈 수 있도록 yield return null; 또는 짧은 대기만 합니다.
            // 만약 페이드 완료를 반드시 기다려야 한다면, SoundManager에 페이드 완료 콜백이나 상태 확인 플래그가 필요합니다.
            // 현재 SoundManager는 코루틴을 반환하지 않으므로, 여기서는 페이드 시간을 대략적으로 기다리거나 즉시 반환합니다.

            if (fadeDuration > 0)
            {
                // 대략적인 페이드 시간만큼 기다릴 수 있지만, 정확한 완료 시점은 아님
                // yield return new WaitForSeconds(fadeDuration);
                yield return null; // 또는 그냥 다음 액션으로 바로 넘어감
            }
            else
            {
                yield return null; // 즉시 전환으로 간주
            }
            Debug.Log($"BGMChangerAction: BGM change request for '{bgmIDToPlay}' sent to SoundManager.");

            CompleteAction(); // 액션 완료 알림
        }
    }
}
