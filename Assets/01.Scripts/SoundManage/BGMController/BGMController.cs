using SoundManage;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float fadeLength = 1f;
    [SerializeField] private int _playIndex = 0;
    public SoundSO[] bgmList;
    private AudioSource[] _audioPlayers;

    private AudioSource _currentAudioSource;

    private void Awake()
    {
        _audioPlayers = GetComponentsInChildren<AudioSource>();

    }

    private void FixedUpdate()
    {

    }

    public void SetBGMs(SoundSO[] list)
    {
        bgmList = list;

    }

    public void ChangeBGM()
    {
        float remainingTime = _currentAudioSource.clip.length - _currentAudioSource.time; // 재생까지 남은 시간
    }

    public void PlayNextAudio()
    {
        
        _currentAudioSource = _audioPlayers[_playIndex];
    }

    public void EndAudio()
    {


    }



}
