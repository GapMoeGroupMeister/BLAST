using UnityEngine;

namespace SoundManage
{
    [CreateAssetMenu(menuName = "SO/SoundSO")]
    public class SoundSO : ScriptableObject
    {
        public string soundName = "None Title";
        public AudioType audioType;
        public AudioClip clip;
        public bool loop;
        public bool randomizePitch = false;
        
        [Range(0, 1f)]
        public float randomPitchModifier = 0.1f;
        [Range(0.1f, 2f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;

    }  
}
