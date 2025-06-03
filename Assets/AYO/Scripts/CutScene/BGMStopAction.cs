// BGMStopAction.cs
using System.Collections;
using UnityEngine;

namespace AYO
{
    public class BGMStopAction : DirectorAction
    {
        [Header("BGM Stop Settings")]
        [Tooltip("BGM을 정지할 때 사용할 페이드 아웃 시간(초). -1이면 SoundManager의 기본값 또는 현재 BGM의 특정 페이드 시간 사용, 0이면 즉시 정지.")]
        [SerializeField] private float fadeOutDuration = -1f;

        public override IEnumerator Execute()
        {
            if (SoundManager.Instance == null)
            {
                Debug.LogError("BGMStopAction: SoundManager 인스턴스를 찾을 수 없습니다!");
                yield break;
            }

            Debug.Log($"BGMStopAction: Stopping BGM with fade out duration {fadeOutDuration}s.");
            SoundManager.Instance.StopBGM(fadeOutDuration);

            // BGM 페이드 아웃이 완료될 때까지 기다릴지 여부는 선택사항입니다.
            // StopBGM 역시 요청만 보내고 바로 다음 액션으로 넘어갈 수 있도록 할 수 있습니다.
            if (fadeOutDuration > 0)
            {
                // 대략적인 페이드 아웃 시간만큼 기다림 (정확한 완료 시점은 아님)
                // yield return new WaitForSeconds(fadeOutDuration);
                yield return null;
            }
            else
            {
                yield return null; // 즉시 정지로 간주
            }
            Debug.Log("BGMStopAction: BGM stop request sent to SoundManager.");

            CompleteAction(); // 액션 완료 알림
        }
    }
}