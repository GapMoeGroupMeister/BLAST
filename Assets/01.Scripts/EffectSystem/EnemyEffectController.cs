using EffectSystem;
using UnityEngine;

namespace EffectSystem
{
    public class EnemyEffectController : AgentEffectController
    {
        [SerializeField] private EnemyEffectStateUI _effectUI;

        public override void ApplyEffect(EffectStateTypeEnum type, float duration, int level, float percent = 1f)
        {
            if(type == 0) return;
            if(!effectDictionary[type].enabled)
                _effectUI.GenerateSlot(type, effectDictionary[type]);
            
            base.ApplyEffect(type, duration, level);
        
        }
    }

}