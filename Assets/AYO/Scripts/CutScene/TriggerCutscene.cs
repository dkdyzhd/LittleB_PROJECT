using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYO;

namespace AYO
{
    public class TriggerCutscene : MonoBehaviour
    {
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private CutsceneSequencer cutsceneToPlay; // 실행할 컷씬 시퀀서 참조
        private bool _hasBeenTriggered = false;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_hasBeenTriggered && collision.CompareTag(playerTag))
            {
                if (cutsceneToPlay != null)
                {
                    _hasBeenTriggered = true;
                    Debug.Log($"Triggering cutscene: {cutsceneToPlay.name}");
                    cutsceneToPlay.Play(); // 시퀀서의 재생 메소드 호출
                }
                else
                {
                    Debug.LogError("TriggerCutscene: CutsceneSequencer is not assigned!");
                }
            }
        }
    }
}
