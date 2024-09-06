using System.Collections;
using Crogen.ObjectPooling;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundManage
{
    public class SoundObject : MonoBehaviour, IPoolingObject
    {
        [field:SerializeField] public PoolType OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set; }
        
        
        [SerializeField] private AudioMixerGroup _sfxGroup, _musicGroup;

        private AudioSource _audioSource;
        public string ItemName => "SoundPlayer";

        public GameObject GetGameObject() => gameObject;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(SoundSO data)
        {

            if (data.audioType == AudioType.SFX)
            {
                _audioSource.outputAudioMixerGroup = _sfxGroup;
            }
            else if (data.audioType == AudioType.BGM)
            {
                _audioSource.outputAudioMixerGroup = _musicGroup;
            }

            _audioSource.volume = data.volume;
            _audioSource.pitch = data.pitch;
            if (data.randomizePitch)
            {
                _audioSource.pitch += Random.Range(-data.randomPitchModifier, data.randomPitchModifier);
            }
            _audioSource.clip = data.clip;

            _audioSource.loop = data.loop;

            if (!data.loop)
            {
                float time = _audioSource.clip.length + .2f;
                StartCoroutine(DisableSoundTimer(time));
            }
            _audioSource.Play();
        }

        private IEnumerator DisableSoundTimer(float time)
        {
            yield return new WaitForSeconds(time);
            this.Push();
        }

        public void StopAndGoToPool()
        {
            _audioSource.Stop();
            this.Push();
        }

       
        
        public void OnPop()
        {
            
        }

        public void OnPush()
        {
        }
    }
}