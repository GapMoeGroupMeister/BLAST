using DG.Tweening;
using SoundManage;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float _fadeLength = 1f;
    [SerializeField] private int _bgmIndex = -1; // -1로 시작해야 0번부터 자동으로 시작된다.
    [SerializeField] private int _playerIndex = 0;
    public SoundSO[] bgmList;
    private AudioSource[] _audioPlayers;

    private AudioSource _currentAudioSource;

    private void Awake()
    {
        _audioPlayers = GetComponentsInChildren<AudioSource>();

    }

    private void Start() {
        PlayNextAudio();
    }

    private void FixedUpdate()
    {
        ChangeBGM();
    }

    public void SetBGMs(SoundSO[] list)
    {
        bgmList = list;

    }

    public void ChangeBGM()
    {
        float remainingTime = _currentAudioSource.clip.length - _currentAudioSource.time; // 재생까지 남은 시간
        if (remainingTime < _fadeLength)
        {
            EndAudio();
            PlayNextAudio();
        }
    }

    public void PlayNextAudio()
    {
        _bgmIndex = (_bgmIndex + 1) % bgmList.Length;
        _playerIndex = (_playerIndex + 1) % _audioPlayers.Length;

        _currentAudioSource = _audioPlayers[_playerIndex];
        _currentAudioSource.clip = bgmList[_bgmIndex].clip;
        _currentAudioSource.Play();
        _currentAudioSource.DOFade(1f, _fadeLength);
    }

    public void EndAudio()
    {
        _currentAudioSource.DOFade(0f, _fadeLength);

    }



}
