using System.Collections;
using UnityEngine;

namespace AYO
{
    public class DialogueAction : DirectorAction // DirectorAction을 상속받습니다.
    {
        [Header("Dialogue Settings")]
        [SerializeField] private DialManager dialManager;         // 대화 시스템 매니저 참조
        [SerializeField] private SpeakingArray dialogueContent;   // 이 액션에서 보여줄 대화 내용 (SpeakingArray)

        /*[Header("Optional")]
        [SerializeField] private string speakerOverride; // 특정 발화자로 강제하고 싶을 경우 (비워두면 SpeakingArray 설정 따름)*/

        void Awake()
        {
            // DialManager를 자동으로 찾거나, 직접 할당되었는지 확인
            if (dialManager == null)
            {
                dialManager = FindObjectOfType<DialManager>(); // 씬에서 DialManager 검색
                if (dialManager == null)
                {
                    Debug.LogError("DialogueAction: DialManager not found in the scene and not assigned!");
                }
            }

            if (dialogueContent == null)
            {
                Debug.LogError($"DialogueAction on {gameObject.name}: DialogueContent (SpeakingArray) is not assigned!");
            }
        }

        // DirectorAction의 추상 메소드 구현
        public override IEnumerator Execute()
        {
            if (dialManager == null)
            {
                Debug.LogError("DialogueAction: Cannot execute, DialManager is missing.");
                yield break; // DialManager 없이는 실행 불가, 코루틴 즉시 종료
            }

            if (dialogueContent == null)
            {
                Debug.LogError("DialogueAction: Cannot execute, DialogueContent (SpeakingArray) is missing.");
                yield break; // 대화 내용 없이는 실행 불가
            }

            Debug.Log($"DialogueAction: Starting dialogue with content '{dialogueContent.name}'.");

            // 만약 발화자 오버라이드가 있다면, SpeakingArray의 첫 번째 발화자를 변경하거나
            // DialManager.ShowDialogue에 발화자 정보를 넘기는 방식이 필요할 수 있습니다.
            // 여기서는 SpeakingArray가 발화자 정보를 포함하고 있다고 가정하고, speakerOverride는 참고용으로 둡니다.
            /*if (!string.IsNullOrEmpty(speakerOverride))
            {
                Debug.Log($"DialogueAction: Speaker override requested: {speakerOverride}. " +
                          $"(Ensure DialManager or SpeakingArray handles this if applicable)");
                // 예: dialogueContent.SetDefaultSpeaker(speakerOverride); (SpeakingArray에 이런 기능이 있다면)
            }*/

            // 대화 시작
            dialManager.ShowDialogue(dialogueContent);

            Debug.Log($"DialogueAction: Starting dialogue. Waiting for full completion (including NextEvent)...");

            // DialManager의 새로운 플래그를 기다림
            yield return new WaitUntil(() => dialManager.IsDialogueAndEventsFullyCompleted);

            Debug.Log($"DialogueAction: Dialogue with content '{dialogueContent.name}' and its NextEvent fully finished.");

            CompleteAction(); // 액션 완료 알림
        }
    }
}
