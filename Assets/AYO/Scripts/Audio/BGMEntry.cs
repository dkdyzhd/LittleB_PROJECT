using UnityEngine;

[System.Serializable]
public class BGMEntry
{
    [Tooltip("BGM을 식별하기 위한 고유한 문자열 ID (예: MainTheme, CaveArea, BossBattle1)")]
    public string bgmID;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    [Tooltip("이 BGM에 대한 개별 볼륨 스케일 (SoundManager의 전체 BGM 볼륨에 곱해짐)")]
    public float volumeScale = 1.0f; // 개별 BGM 볼륨 조절값
    [Tooltip("이 BGM에 대한 특정 페이드 인/아웃 시간. -1이면 SoundManager의 기본값 사용.")]
    public float specificFadeDuration = -1f; // 개별 페이드 시간
    // 필요에 따라 추가 정보 (예: 루프 여부는 AudioSource에서 기본 설정)
}