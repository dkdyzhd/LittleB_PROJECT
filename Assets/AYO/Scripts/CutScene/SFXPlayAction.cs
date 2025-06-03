using UnityEngine;
using System.Collections;

namespace AYO
{
    public class SFXPlayAction : DirectorAction
    {
        [Header("SFX Settings")]
        [Tooltip("재생할 오디오 클립")]
        [SerializeField] private AudioClip audioClip;
        
        [Tooltip("true이면 3D SFX로, false이면 2D SFX로 재생")]
        [SerializeField] private bool playAs3DSound = true;
        
        [Tooltip("SFX 루프 재생 여부")]
        [SerializeField] private bool isLooping = false;

        [Header("2D SFX Options")]
        [Tooltip("2D SFX용 AudioSource (playAs3DSound가 false일 때 필요)")]
        [SerializeField] private AudioSource audioSource2D;

        [Header("3D SFX Options")]
        [Tooltip("3D SFX가 발생할 위치를 지정할 Transform. 비워두면 이 게임 오브젝트의 위치를 사용.")]
        [SerializeField] private Transform sfxPositionTransform;
        
        [Tooltip("sfxPositionTransform이 없을 경우 사용될 절대 좌표.")]
        [SerializeField] private Vector3 sfxAbsolutePosition = Vector3.zero;
        
        [Tooltip("3D SFX 재생 시 소리를 붙일 부모 Transform (선택적)")]
        [SerializeField] private Transform sfxParentTransform;

        [Header("Common SFX Options")]
        [Range(0f, 2f)]
        [Tooltip("SFX 볼륨 스케일")]
        [SerializeField] private float volumeScale = 1.0f;

        [Range(0.1f, 3f)]
        [Tooltip("SFX 재생 피치 (1.0이 기본)")]
        [SerializeField] private float pitch = 1.0f;

        [Header("Looping Options")]
        [Tooltip("루프 SFX를 정지시키기 전까지 대기할 시간(초). 0 이하면 수동 정지 필요.")]
        [SerializeField] private float stopLoopingAfterDuration = 0f;

        private AudioSource _activeLoopingSource; // 루프 재생 중인 AudioSource

        public override IEnumerator Execute()
        {
            if (audioClip == null)
            {
                Debug.LogWarning($"SFXPlayAction ({gameObject.name}): AudioClip이 지정되지 않았습니다.", this);
                CompleteAction();
                yield break;
            }

            if (!playAs3DSound && audioSource2D == null)
            {
                Debug.LogError($"SFXPlayAction ({gameObject.name}): 2D SFX 재생을 위한 AudioSource가 지정되지 않았습니다.", this);
                CompleteAction();
                yield break;
            }

            // 실제 재생 로직
            if (playAs3DSound)
            {
                // 3D 사운드 재생
                Vector3 positionToPlay = GetPlayPosition();
                
                if (SoundManager.Instance == null)
                {
                    Debug.LogError($"SFXPlayAction ({gameObject.name}): SoundManager를 찾을 수 없습니다.", this);
                    CompleteAction();
                    yield break;
                }

                Debug.Log($"SFXPlayAction ({gameObject.name}): Playing 3D SFX '{audioClip.name}' at {positionToPlay}. Loop: {isLooping}");
                
                if (isLooping)
                {
                    _activeLoopingSource = SoundManager.Instance.Play3DLoopingSound(
                        audioClip, positionToPlay, sfxParentTransform, volumeScale
                    );
                    
                    if (_activeLoopingSource != null)
                    {
                        _activeLoopingSource.pitch = pitch;
                    }
                }
                else
                {
                    AudioSource source = SoundManager.Instance.Play3DSoundAtPoint(
                        audioClip, positionToPlay, volumeScale, sfxParentTransform
                    );
                    
                    if (source != null)
                    {
                        source.pitch = pitch;
                    }
                }
            }
            else
            {
                // 2D 사운드 재생
                Debug.Log($"SFXPlayAction ({gameObject.name}): Playing 2D SFX '{audioClip.name}'. Loop: {isLooping}");
                
                if (isLooping)
                {
                    // 2D 루프 재생
                    audioSource2D.clip = audioClip;
                    audioSource2D.volume = volumeScale * (SoundManager.Instance != null ? SoundManager.Instance.MasterSfxVolume : 1f);
                    audioSource2D.pitch = pitch;
                    audioSource2D.loop = true;
                    audioSource2D.Play();
                    _activeLoopingSource = audioSource2D;
                }
                else
                {
                    // 2D 원샷 재생
                    audioSource2D.pitch = pitch;
                    audioSource2D.PlayOneShotScaled(audioClip, volumeScale);
                }
            }

            // 루프 처리
            if (isLooping && _activeLoopingSource != null)
            {
                if (stopLoopingAfterDuration > 0)
                {
                    Debug.Log($"SFXPlayAction ({gameObject.name}): Looping SFX '{audioClip.name}' 재생 시작. {stopLoopingAfterDuration}초 후 정지 예정.");
                    yield return new WaitForSeconds(stopLoopingAfterDuration);
                    StopLoopingSFX();
                }
                else
                {
                    Debug.Log($"SFXPlayAction ({gameObject.name}): Looping SFX '{audioClip.name}' 재생 시작 (수동 정지 필요).");
                }
            }

            CompleteAction();
            Debug.Log($"SFXPlayAction ({gameObject.name}): SFX '{audioClip.name}' 액션 처리 완료.");
        }

        private Vector3 GetPlayPosition()
        {
            if (sfxPositionTransform != null)
                return sfxPositionTransform.position;
            
            if (sfxAbsolutePosition != Vector3.zero)
                return sfxAbsolutePosition;
            
            return transform.position;
        }

        // 외부에서 루프 사운드를 중지하는 메소드
        public void StopLoopingSFX()
        {
            if (_activeLoopingSource == null) return;

            Debug.Log($"SFXPlayAction ({gameObject.name}): 루프 SFX '{audioClip?.name}' 정지.");
            
            if (playAs3DSound && SoundManager.Instance != null)
            {
                // 3D 사운드는 풀에 반환
                SoundManager.Instance.Stop3DSound(_activeLoopingSource);
            }
            else
            {
                // 2D 사운드는 단순 정지
                _activeLoopingSource.Stop();
                _activeLoopingSource.loop = false;
                _activeLoopingSource.clip = null;
            }
            
            _activeLoopingSource = null;
        }

        // 액션이 비활성화되거나 파괴될 때 루프 사운드 정지
        private void OnDisable()
        {
            if (_activeLoopingSource != null && isLooping)
            {
                StopLoopingSFX();
            }
        }

        private void OnDestroy()
        {
            if (_activeLoopingSource != null && isLooping)
            {
                StopLoopingSFX();
            }
        }
    }
}