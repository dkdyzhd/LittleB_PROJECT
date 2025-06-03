using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Linq 사용

[CreateAssetMenu(fileName = "SFXLibrary", menuName = "ProjectLB/Audio/SFX Library")]
public class SFXLibrary : ScriptableObject
{
    public List<SFXEntry> sfxList = new List<SFXEntry>();

    // 런타임에 빠른 접근을 위한 딕셔너리
    private Dictionary<string, SFXEntry> _sfxDictionary;

    // 게임 시작 시 또는 필요할 때 호출하여 딕셔너리 초기화
    public void Initialize()
    {
        _sfxDictionary = new Dictionary<string, SFXEntry>();
        foreach (var entry in sfxList)
        {
            if (entry.audioClip == null)
            {
                Debug.LogWarning($"SFXLibrary: '{entry.sfxName}'의 AudioClip이 할당되지 않았습니다.");
                continue;
            }
            if (!_sfxDictionary.ContainsKey(entry.sfxName))
            {
                _sfxDictionary.Add(entry.sfxName, entry);
            }
            else
            {
                Debug.LogWarning($"SFXLibrary: 중복된 SFX 이름 '{entry.sfxName}'이(가) 있습니다. 첫 번째 항목만 사용됩니다.");
            }
        }
    }

    public SFXEntry GetSFX(string sfxName)
    {
        if (_sfxDictionary == null)
        {
            Debug.LogError("SFXLibrary가 초기화되지 않았습니다. SoundManager에서 Initialize()를 호출했는지 확인하세요.");
            return null;
        }
        _sfxDictionary.TryGetValue(sfxName, out SFXEntry entry);
        if (entry == null)
        {
            Debug.LogWarning($"SFX '{sfxName}'을(를) 라이브러리에서 찾을 수 없습니다.");
        }
        return entry;
    }
}