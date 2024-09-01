using System.Collections.Generic;
using UnityEngine;

namespace SoundManage
{
    [CreateAssetMenu(menuName = "SO/SoundTableSO")]
    public class SoundTableSO : ScriptableObject
    {
        public AudioType audioType;
        public List<SoundSO> soundSOList;
    }
}