using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BGMLibrary", menuName = "ProjectLB/Audio/BGM Library")]
public class BGMLibrary : ScriptableObject
{
    public List<BGMEntry> bgmList = new List<BGMEntry>();

    // 런타임 시 빠른 검색을 위한 딕셔너리
    private Dictionary<string, BGMEntry> _bgmDictionary;
    private bool _isInitialized = false;

    private void OnEnable()
    {
        _isInitialized = false;
    }

    public void Initialize()
    {
        if (_isInitialized) return;

        _bgmDictionary = new Dictionary<string, BGMEntry>();

        foreach (var entry in bgmList)
        {
            if (string.IsNullOrEmpty(entry.bgmID))
            {
                Debug.LogWarning($"[BGMLibrary] bgmID가 비어있는 BGMEntry가 있습니다. (AudioClip: {(entry.audioClip != null ? entry.audioClip.name : "null")}) 이 항목은 ID로 접근할 수 없습니다.");
                continue;
            }
            if (entry.audioClip == null)
            {
                Debug.LogWarning($"[BGMLibrary] BGM ID '{entry.bgmID}'의 AudioClip이 할당되지 않았습니다.");
                // continue; // 클립이 없어도 ID는 등록될 수 있게 하거나, 여기서 제외할 수 있음
            }

            if (!_bgmDictionary.ContainsKey(entry.bgmID))
            {
                _bgmDictionary.Add(entry.bgmID, entry);
            }
            else
            {
                Debug.LogWarning($"[BGMLibrary] 중복된 BGM ID '{entry.bgmID}'가 감지되었습니다. 첫 번째 항목만 사용됩니다.");
            }
        }
        _isInitialized = true;
        Debug.Log("[BGMLibrary] 초기화 완료.");

    }

    public BGMEntry GetBGMEntry(string bgmID)
    {
        if (!_isInitialized)
        {
            Debug.LogError("[BGMLibrary] 아직 초기화되지 않았습니다. SoundManager.Awake()에서 Initialize()를 호출하는지 확인하세요.");
            return null;
        }

        if (_bgmDictionary.TryGetValue(bgmID, out BGMEntry entry))
        {
            return entry;
        }
        else
        {
            Debug.LogWarning($"[BGMLibrary] BGM ID '{bgmID}'을(를) 찾을 수 없습니다.");
            return null;
        }
    }
}