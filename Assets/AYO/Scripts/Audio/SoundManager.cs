using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// BGM 전환 타입
public enum BGMTransitionType
{
    Crossfade,      // 기존 방식 (동시 페이드)
    Sequential,     // 새로운 방식 (순차 페이드)
    Immediate       // 즉시 전환
}

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance { get; private set; }
    #endregion

    #region BGM Settings
    [Header("BGM Settings")]
    [SerializeField] private AudioSource bgmAudioSource1;
    [SerializeField] private AudioSource bgmAudioSource2;
    [SerializeField] private BGMLibrary bgmLibrary;
    [SerializeField] private float defaultBgmFadeDuration = 1.0f;
    [Range(0f, 1f)]
    [SerializeField] private float masterBgmVolume = 1.0f;

    [Header("BGM AutoPlay on Start")]
    [SerializeField] private bool playBgmOnStart = false;
    [SerializeField] private string startBgmID;
    [SerializeField] private float startBgmFadeInDuration = 0f;
    [SerializeField] private BGMTransitionType defaultTransitionType = BGMTransitionType.Sequential; // 기본 전환 타입
    #endregion

    #region Global Audio Settings
    [Header("Global Audio Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float masterSfxVolume = 1.0f;
    #endregion

    #region 3D Audio Pooling
    [Header("3D Audio Pooling (Optional)")]
    [SerializeField] private GameObject audioSource3DPrefab;
    [SerializeField] private int pool3DSize = 10;
    
    private List<AudioSource> _pool3D;
    private GameObject _poolParent;
    private int _nextPoolIndex = 0;
    #endregion

    private AudioSource _activeBgmSource;
    private AudioSource _inactiveBgmSource;
    private Coroutine _bgmTransitionCoroutine;
    private BGMEntry _currentBgmEntry;

    #region Unity Lifecycle
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeBgmSources();
        Initialize3DPool();
    }

    void Start()
    {
        if (playBgmOnStart && bgmLibrary != null && !string.IsNullOrEmpty(startBgmID))
        {
            PlayBGM(startBgmID, defaultTransitionType, startBgmFadeInDuration);
        }
    }
    #endregion

    #region Initialization
    private void InitializeBgmSources()
    {
        if (bgmAudioSource1 == null || bgmAudioSource2 == null)
        {
            Debug.LogError("[SoundManager] BGM AudioSource(s) not assigned!", this);
            enabled = false;
            return;
        }
        
        ConfigureAudioSource(bgmAudioSource1, true, 0f);
        ConfigureAudioSource(bgmAudioSource2, true, 0f);

        _activeBgmSource = bgmAudioSource1;
        _inactiveBgmSource = bgmAudioSource2;

        if (bgmLibrary == null) 
            Debug.LogError("[SoundManager] BGMLibrary not assigned!", this);
        else 
            bgmLibrary.Initialize();

        Debug.Log("[SoundManager] BGM system initialized.");
    }

    private void Initialize3DPool()
    {
        if (audioSource3DPrefab == null) 
        {
            Debug.LogWarning("[SoundManager] 3D Audio prefab not assigned. 3D sound pooling disabled.", this);
            return;
        }

        _pool3D = new List<AudioSource>(pool3DSize);
        _poolParent = new GameObject("3D_Audio_Pool");
        _poolParent.transform.SetParent(this.transform);

        for (int i = 0; i < pool3DSize; i++)
        {
            GameObject obj = Instantiate(audioSource3DPrefab, _poolParent.transform);
            AudioSource source = obj.GetComponent<AudioSource>();
            
            if (source != null)
            {
                obj.name = $"3D_Audio_{i}";
                ConfigureAudioSource(source, false, 1f);
                source.spatialBlend = 1.0f; // 3D sound
                obj.SetActive(false);
                _pool3D.Add(source);
            }
            else
            {
                Debug.LogError($"[SoundManager] 3D Audio prefab missing AudioSource component!", this);
                Destroy(obj);
            }
        }
        
        Debug.Log($"[SoundManager] 3D Audio pool initialized with {_pool3D.Count} sources.");
    }

    private void ConfigureAudioSource(AudioSource source, bool loop, float volume)
    {
        source.playOnAwake = false;
        source.loop = loop;
        source.volume = volume;
    }
    #endregion

    #region BGM Control
    // 메인 BGM 재생 메서드 - 전환 타입 파라미터 추가
    public void PlayBGM(string bgmID, BGMTransitionType transitionType = BGMTransitionType.Sequential, float fadeDurationOverride = -1f)
    {
        if (bgmLibrary == null) 
        {
            Debug.LogError("[SoundManager] BGMLibrary is null. Cannot play BGM.");
            return;
        }

        BGMEntry entry = bgmLibrary.GetBGMEntry(bgmID);
        if (entry == null || entry.audioClip == null)
        {
            Debug.LogWarning($"[SoundManager] BGM ID '{bgmID}' not found or has no AudioClip.");
            return;
        }

        float targetVolume = Mathf.Clamp01(masterBgmVolume * entry.volumeScale);
        float fadeDuration = (fadeDurationOverride >= 0) ? fadeDurationOverride :
                            (entry.specificFadeDuration >= 0 ? entry.specificFadeDuration : defaultBgmFadeDuration);

        // 같은 곡이 이미 재생 중인지 확인
        if (_activeBgmSource.isPlaying && _activeBgmSource.clip == entry.audioClip && 
            Mathf.Approximately(_activeBgmSource.volume, targetVolume))
        {
            Debug.Log($"[SoundManager] BGM '{bgmID}' is already playing at target volume.");
            _currentBgmEntry = entry;
            return;
        }

        if (_bgmTransitionCoroutine != null) 
            StopCoroutine(_bgmTransitionCoroutine);

        _currentBgmEntry = entry;
        
        Debug.Log($"[SoundManager] Playing BGM: '{bgmID}' with {transitionType} transition. Fade: {fadeDuration:F2}s");
        
        // 전환 타입에 따라 다른 코루틴 실행
        switch (transitionType)
        {
            case BGMTransitionType.Crossfade:
                _bgmTransitionCoroutine = StartCoroutine(PerformBgmCrossfade(entry.audioClip, targetVolume, fadeDuration));
                break;
            case BGMTransitionType.Sequential:
                _bgmTransitionCoroutine = StartCoroutine(PerformBgmSequential(entry.audioClip, targetVolume, fadeDuration));
                break;
            case BGMTransitionType.Immediate:
                _bgmTransitionCoroutine = StartCoroutine(PerformBgmImmediate(entry.audioClip, targetVolume));
                break;
        }
    }

    // 오버로드 - 기본 전환 타입 사용
    public void PlayBGM(string bgmID, float fadeDurationOverride)
    {
        PlayBGM(bgmID, defaultTransitionType, fadeDurationOverride);
    }

    // 기존 크로스페이드 방식 (동시 페이드)
    private IEnumerator PerformBgmCrossfade(AudioClip newClip, float targetVolume, float duration)
    {
        _inactiveBgmSource.clip = newClip;
        _inactiveBgmSource.volume = 0f;
        _inactiveBgmSource.Play();

        float timer = 0f;
        float activeStartVolume = _activeBgmSource.isPlaying ? _activeBgmSource.volume : 0f;

        if (duration <= Mathf.Epsilon)
        {
            if (_activeBgmSource.isPlaying) _activeBgmSource.Stop();
            _activeBgmSource.volume = 0f;
            _inactiveBgmSource.volume = targetVolume;
        }
        else
        {
            while (timer < duration)
            {
                timer += Time.deltaTime;
                float progress = Mathf.Clamp01(timer / duration);
                
                _activeBgmSource.volume = Mathf.Lerp(activeStartVolume, 0f, progress);
                _inactiveBgmSource.volume = Mathf.Lerp(0f, targetVolume, progress);
                
                yield return null;
            }
            
            _activeBgmSource.volume = 0f;
            _inactiveBgmSource.volume = targetVolume;
        }

        if (_activeBgmSource.isPlaying) _activeBgmSource.Stop();
        
        // 소스 교체
        AudioSource temp = _activeBgmSource;
        _activeBgmSource = _inactiveBgmSource;
        _inactiveBgmSource = temp;

        _bgmTransitionCoroutine = null;
        Debug.Log($"[SoundManager] BGM crossfade to '{newClip.name}' complete.");
    }

    // 새로운 순차 페이드 방식 (페이드아웃 → 페이드인)
    private IEnumerator PerformBgmSequential(AudioClip newClip, float targetVolume, float duration)
    {
        float halfDuration = duration * 0.5f;
        
        // 1. 현재 BGM 페이드아웃
        if (_activeBgmSource.isPlaying)
        {
            float startVolume = _activeBgmSource.volume;
            float timer = 0f;
            
            while (timer < halfDuration)
            {
                timer += Time.deltaTime;
                _activeBgmSource.volume = Mathf.Lerp(startVolume, 0f, timer / halfDuration);
                yield return null;
            }
            
            _activeBgmSource.volume = 0f;
            _activeBgmSource.Stop();
        }
        
        // 2. 새 BGM 설정 및 페이드인
        _inactiveBgmSource.clip = newClip;
        _inactiveBgmSource.volume = 0f;
        _inactiveBgmSource.Play();
        
        float fadeInTimer = 0f;
        while (fadeInTimer < halfDuration)
        {
            fadeInTimer += Time.deltaTime;
            _inactiveBgmSource.volume = Mathf.Lerp(0f, targetVolume, fadeInTimer / halfDuration);
            yield return null;
        }
        
        _inactiveBgmSource.volume = targetVolume;
        
        // 소스 교체
        AudioSource temp = _activeBgmSource;
        _activeBgmSource = _inactiveBgmSource;
        _inactiveBgmSource = temp;
        
        _bgmTransitionCoroutine = null;
        Debug.Log($"[SoundManager] BGM sequential transition to '{newClip.name}' complete.");
    }

    // 즉시 전환
    private IEnumerator PerformBgmImmediate(AudioClip newClip, float targetVolume)
    {
        if (_activeBgmSource.isPlaying)
        {
            _activeBgmSource.Stop();
            _activeBgmSource.volume = 0f;
        }
        
        _inactiveBgmSource.clip = newClip;
        _inactiveBgmSource.volume = targetVolume;
        _inactiveBgmSource.Play();
        
        // 소스 교체
        AudioSource temp = _activeBgmSource;
        _activeBgmSource = _inactiveBgmSource;
        _inactiveBgmSource = temp;
        
        yield return null;
        
        _bgmTransitionCoroutine = null;
        Debug.Log($"[SoundManager] BGM immediate switch to '{newClip.name}' complete.");
    }

    public void StopBGM(float fadeDurationOverride = -1f)
    {
        if (!_activeBgmSource.isPlaying || _activeBgmSource.clip == null) return;

        float fadeDuration = (fadeDurationOverride >= 0) ? fadeDurationOverride :
                            (_currentBgmEntry != null && _currentBgmEntry.specificFadeDuration >= 0 ? 
                            _currentBgmEntry.specificFadeDuration : defaultBgmFadeDuration);

        if (_bgmTransitionCoroutine != null) 
            StopCoroutine(_bgmTransitionCoroutine);

        Debug.Log($"[SoundManager] Stopping BGM: '{_activeBgmSource.clip.name}'. Fade: {fadeDuration:F2}s");
        _bgmTransitionCoroutine = StartCoroutine(PerformBgmFadeOut(_activeBgmSource, fadeDuration));
        _currentBgmEntry = null;
    }

    private IEnumerator PerformBgmFadeOut(AudioSource sourceToFade, float duration)
    {
        float startVolume = sourceToFade.volume;
        float timer = 0f;
        AudioClip clipNameForLog = sourceToFade.clip;

        if (duration <= Mathf.Epsilon)
        {
            sourceToFade.volume = 0f;
            sourceToFade.Stop();
            sourceToFade.clip = null;
        }
        else
        {
            while (timer < duration)
            {
                timer += Time.deltaTime;
                sourceToFade.volume = Mathf.Lerp(startVolume, 0f, timer / duration);
                yield return null;
            }
            sourceToFade.volume = 0f;
            sourceToFade.Stop();
            sourceToFade.clip = null;
        }
        
        _bgmTransitionCoroutine = null;
        Debug.Log($"[SoundManager] BGM '{clipNameForLog?.name}' fade out complete.");
    }

    public void SetMasterBgmVolume(float volume)
    {
        masterBgmVolume = Mathf.Clamp01(volume);
        if (_activeBgmSource.isPlaying && _currentBgmEntry != null)
        {
            _activeBgmSource.volume = Mathf.Clamp01(masterBgmVolume * _currentBgmEntry.volumeScale);
        }
        Debug.Log($"[SoundManager] Master BGM Volume set to: {masterBgmVolume:F2}");
    }

    public BGMEntry GetCurrentPlayingBGMEntry() => _currentBgmEntry;
    #endregion

    #region Global Audio Control
    public void SetMasterSfxVolume(float volume)
    {
        masterSfxVolume = Mathf.Clamp01(volume);
        Debug.Log($"[SoundManager] Master SFX Volume set to: {masterSfxVolume:F2}");
    }

    public float MasterSfxVolume => masterSfxVolume;
    public float MasterBgmVolume => masterBgmVolume;
    #endregion

    #region 3D Audio Pool (Optional)
    // 3D 사운드가 필요한 경우에만 사용
    public AudioSource Play3DSoundAtPoint(AudioClip clip, Vector3 position, float volumeScale = 1.0f, Transform parent = null)
    {
        if (clip == null || _pool3D == null || _pool3D.Count == 0)
            return null;

        AudioSource source = GetAvailable3DSource();
        if (source != null)
        {
            source.gameObject.SetActive(true);
            source.transform.position = position;
            source.transform.SetParent(parent != null ? parent : _poolParent.transform);
            
            source.clip = clip;
            source.volume = volumeScale * masterSfxVolume;
            source.Play();
            
            StartCoroutine(Return3DSourceAfterPlay(source));
            return source;
        }
        
        Debug.LogWarning("[SoundManager] No available 3D audio source in pool.");
        return null;
    }

    // 루핑 3D 사운드 (수동으로 중지 필요)
    public AudioSource Play3DLoopingSound(AudioClip clip, Vector3 position, Transform parent = null, float volumeScale = 1.0f)
    {
        if (clip == null || _pool3D == null || _pool3D.Count == 0)
            return null;

        AudioSource source = GetAvailable3DSource();
        if (source != null)
        {
            source.gameObject.SetActive(true);
            source.transform.position = position;
            source.transform.SetParent(parent != null ? parent : _poolParent.transform);
            
            source.clip = clip;
            source.volume = volumeScale * masterSfxVolume;
            source.loop = true;
            source.Play();
            
            // 루핑 사운드는 자동 반환하지 않음
            return source;
        }
        
        return null;
    }

    public void Stop3DSound(AudioSource source)
    {
        if (source != null && _pool3D != null && _pool3D.Contains(source))
        {
            source.Stop();
            source.loop = false;
            source.clip = null;
            source.gameObject.SetActive(false);
            source.transform.SetParent(_poolParent.transform);
        }
    }

    private AudioSource GetAvailable3DSource()
    {
        if (_pool3D == null) return null;
        
        for (int i = 0; i < _pool3D.Count; i++)
        {
            int idx = (_nextPoolIndex + i) % _pool3D.Count;
            if (!_pool3D[idx].gameObject.activeInHierarchy)
            {
                _nextPoolIndex = (idx + 1) % _pool3D.Count;
                return _pool3D[idx];
            }
        }
        
        // 모두 사용 중이면 가장 오래된 것 재사용
        _nextPoolIndex = (_nextPoolIndex + 1) % _pool3D.Count;
        Stop3DSound(_pool3D[_nextPoolIndex]);
        return _pool3D[_nextPoolIndex];
    }

    private IEnumerator Return3DSourceAfterPlay(AudioSource source)
    {
        if (source.clip != null)
            yield return new WaitForSeconds(source.clip.length);
        
        Stop3DSound(source);
    }
    #endregion
}

// AudioSource 확장 메서드 - 각 객체에서 간편하게 사용
public static class AudioSourceExtensions
{
    public static void PlayOneShotScaled(this AudioSource source, AudioClip clip, float volumeScale = 1.0f)
    {
        if (source != null && clip != null && SoundManager.Instance != null)
        {
            source.PlayOneShot(clip, volumeScale * SoundManager.Instance.MasterSfxVolume);
        }
    }
}