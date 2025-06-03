using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewImageDisplayData", menuName = "AYO/Cutscene/Image Display Data")]
public class ImageDisplayDataSO : ScriptableObject
{
    [Tooltip("표시할 Sprite 이미지입니다.")]
    public Sprite imageContent;

    [TextArea(3, 10)]
    [Tooltip("표시할 텍스트 내용입니다. (선택 사항)")]
    public string textContent;

    // 필요하다면 추가 데이터:
    // public string itemID;
    // public AudioClip voiceLine;
}