using UnityEngine;

namespace AYO
{
    public class BGMTriggerZone : MonoBehaviour
    {
        [Tooltip("BGMLibrary에 정의된 재생할 BGM의 ID")]
        [SerializeField] private string bgmIDToPlay;

        [Tooltip("이 존에 진입 시 사용할 페이드 시간. -1이면 SoundManager 또는 BGMEntry의 기본값 사용. 0이면 즉시 변경.")]
        [SerializeField] private float fadeInDurationOnEnter = -1f;

        [Header("On Exit Settings (Optional)")]
        [Tooltip("체크하면 플레이어가 존을 벗어날 때 BGM을 변경하거나 멈춥니다.")]
        [SerializeField] private bool changeBgmOnExit = false;
        [Tooltip("플레이어가 존을 벗어날 때 재생할 BGM ID (비워두면 BGM 정지)")]
        [SerializeField] private string bgmIDToPlayOnExit = "";
        [Tooltip("존 퇴장 시 사용할 페이드 아웃/전환 시간")]
        [SerializeField] private float fadeDurationOnExit = -1f;

        private bool _isPlayerInside = false; // 플레이어가 존 내부에 있는지 여부 (중복 진입 방지용)

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isPlayerInside) return; // 이미 플레이어가 안에 있다면 중복 실행 방지

            if (other.CompareTag("Player"))
            {
                _isPlayerInside = true; // 플레이어 진입

                if (SoundManager.Instance != null)
                {
                    if (string.IsNullOrEmpty(bgmIDToPlay))
                    {
                        Debug.LogWarning($"[BGMTriggerZone] '{this.gameObject.name}': 재생할 BGM ID가 지정되지 않았습니다.", this.gameObject);
                        return;
                    }

                    // 현재 재생 중인 BGM이 이 존에서 재생하려는 BGM과 이미 같은 ID인지 확인
                    BGMEntry currentBgm = SoundManager.Instance.GetCurrentPlayingBGMEntry();
                    if (currentBgm != null && currentBgm.bgmID == bgmIDToPlay)
                    {
                        Debug.Log($"[BGMTriggerZone] '{this.gameObject.name}': BGM ID '{bgmIDToPlay}'은(는) 이미 재생 중입니다.", this.gameObject);
                        return;
                    }

                    Debug.Log($"[BGMTriggerZone] '{this.gameObject.name}': 플레이어 진입. BGM '{bgmIDToPlay}' 재생 요청. 페이드 시간: {fadeInDurationOnEnter}s", this.gameObject);
                    SoundManager.Instance.PlayBGM(bgmIDToPlay, fadeInDurationOnEnter);
                }
                else
                {
                    Debug.LogWarning("[BGMTriggerZone] SoundManager 인스턴스를 찾을 수 없습니다.");
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInside = false; // 플레이어 퇴장 시 플래그 리셋
                Debug.Log($"[BGMTriggerZone] '{this.gameObject.name}': 플레이어 퇴장.", this.gameObject);

                if (changeBgmOnExit && SoundManager.Instance != null)
                {
                    if (string.IsNullOrEmpty(bgmIDToPlayOnExit))
                    {
                        Debug.Log($"[BGMTriggerZone] '{this.gameObject.name}': 플레이어 퇴장. BGM 정지 요청.", this.gameObject);
                        SoundManager.Instance.StopBGM(fadeDurationOnExit);
                    }
                    else
                    {
                        Debug.Log($"[BGMTriggerZone] '{this.gameObject.name}': 플레이어 퇴장. BGM '{bgmIDToPlayOnExit}' 재생 요청.", this.gameObject);
                        SoundManager.Instance.PlayBGM(bgmIDToPlayOnExit, fadeDurationOnExit);
                    }
                }
            }
        }

        // 디버깅을 위한 시각적 표시
        private void OnDrawGizmos()
        {
            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                Gizmos.color = _isPlayerInside ? new Color(0, 1, 0, 0.3f) : new Color(1, 1, 0, 0.3f);
                
                if (col is BoxCollider2D box)
                {
                    Matrix4x4 oldMatrix = Gizmos.matrix;
                    Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
                    Gizmos.DrawCube(box.offset, box.size);
                    Gizmos.color = _isPlayerInside ? Color.green : Color.yellow;
                    Gizmos.DrawWireCube(box.offset, box.size);
                    Gizmos.matrix = oldMatrix;
                }
                else if (col is CircleCollider2D circle)
                {
                    Gizmos.DrawSphere(transform.position + (Vector3)circle.offset, circle.radius * transform.lossyScale.x);
                    Gizmos.color = _isPlayerInside ? Color.green : Color.yellow;
                    Gizmos.DrawWireSphere(transform.position + (Vector3)circle.offset, circle.radius * transform.lossyScale.x);
                }
            }
        }

        // Inspector에서 설정을 검증하는 메서드
        private void OnValidate()
        {
            if (fadeInDurationOnEnter < -1f)
                fadeInDurationOnEnter = -1f;
            
            if (fadeDurationOnExit < -1f)
                fadeDurationOnExit = -1f;
        }
    }
}