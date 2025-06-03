using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace AYO
{
    public class CutsceneSequencer : MonoBehaviour
    {
        [Header("Sequencer Settings")]
        [SerializeField] private List<DirectorAction> actionsToPerform = new List<DirectorAction>();
        [SerializeField] private bool playOnStart = false;
        [SerializeField] private bool playOnce = true;

        [Tooltip("전체 시퀀스가 완료된 후 호출될 이벤트입니다.")]
        [SerializeField] private UltEvent onSequenceCompleted;

        [Header("Player Control Dependencies")]
        [SerializeField] private PlayerInputEventManager playerInputManager;
        [SerializeField] private PlayerController playerCharacter;

        private bool _hasPlayed = false;
        private bool _isPlaying = false;
        private Coroutine _currentSequenceCoroutine;

        public bool IsPlaying => _isPlaying;
        public bool HasPlayed => _hasPlayed;

        void Awake()
        {
            if (playerInputManager == null)
            {
                playerInputManager = FindObjectOfType<PlayerInputEventManager>();
            }

            if (playerCharacter == null)
            {
                playerCharacter = FindObjectOfType<PlayerController>();
            }
        }

        void Start()
        {
            if (playOnStart)
            {
                Play();
            }
        }

        public void Play()
        {
            if (_isPlaying)
            {
                // Debug.LogWarning($"Cutscene '{name}' is already playing.");
                return;
            }
            if (playOnce && _hasPlayed)
            {
                // Debug.Log($"Cutscene '{name}' has already been played and is set to play once.");
                return;
            }

            if (_currentSequenceCoroutine != null)
            {
                StopCoroutine(_currentSequenceCoroutine);
            }
            _currentSequenceCoroutine = StartCoroutine(ExecuteSequence());
        }

        private IEnumerator ExecuteSequence()
        {
            _isPlaying = true;
            _hasPlayed = true;

            if (playerInputManager != null && playerCharacter != null)
            {
                // Debug.Log($"Cutscene '{name}': Disabling player input for '{playerCharacter.name}'.");
                playerInputManager.NavigateTarget = null;
                playerCharacter.SetControl(false);
            }

            foreach (var action in actionsToPerform)
            {
                if (action == null)
                {
                    // Debug.LogWarning($"Cutscene '{name}': Found a null action in the sequence. Skipping.");
                    continue;
                }
                // Debug.Log($"Cutscene '{name}': Executing action '{action.GetType().Name}' on GameObject '{action.gameObject.name}'.");
                yield return StartCoroutine(action.Execute());
                // Debug.Log($"Cutscene '{name}': Finished action '{action.GetType().Name}' (Execute coroutine completed).");
            }

            if (playerInputManager != null && playerCharacter != null)
            {
                // Debug.Log($"Cutscene '{name}': Enabling player input for '{playerCharacter.name}'.");
                playerInputManager.NavigateTarget = playerCharacter;
                playerCharacter.SetControl(true);
            }

            _isPlaying = false;
            _currentSequenceCoroutine = null;
            // Debug.Log($"Cutscene '{name}': Sequence finished.");

            onSequenceCompleted?.Invoke();
        }

        public void StopCutscene()
        {
            if (!_isPlaying)
            {
                // Debug.Log($"Cutscene '{name}': Not currently playing, stop command ignored.");
                return;
            }
            
            // Debug.Log($"Cutscene '{name}': Attempting to stop forcefully.");
            if (_currentSequenceCoroutine != null)
            {
                StopCoroutine(_currentSequenceCoroutine);
                _currentSequenceCoroutine = null;
            }
            _isPlaying = false;

            if (playerInputManager != null && playerCharacter != null)
            {
                playerInputManager.NavigateTarget = playerCharacter;
                playerCharacter.SetControl(true);
                // Debug.Log($"Cutscene '{name}': Player input restored for '{playerCharacter.name}'.");
            }
            // Debug.Log($"Cutscene '{name}': Forcefully stopped.");
        }
        
        // 외부에서 nextEvent를 직접 호출할 필요가 없어졌으므로 제거하거나,
        // 특별한 용도가 있다면 유지. 여기서는 제거하는 방향으로 함.
        // public void InvokeNextEvent()
        // {
        //     onSequenceCompleted?.Invoke();
        // }
    }
}