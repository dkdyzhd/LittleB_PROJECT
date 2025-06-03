using UnityEngine;

[System.Serializable] // Inspector에 노출되도록 함
public class SFXEntry
{
    public string sfxName;       // 엑셀의 "파일 이름"에 해당
    public AudioClip audioClip;  // 실제 오디오 클립
    [Range(0f, 1f)]
    public float defaultVolume = 1f; // 기본 볼륨
    public bool isLooping = false;   // 루프 여부 (필요하다면)
    // 필요에 따라 추가 정보 (예: 기본 피치, 3D 사운드 설정 등)
}
