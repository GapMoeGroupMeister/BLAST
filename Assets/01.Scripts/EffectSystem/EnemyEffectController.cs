using System.Collections;
using System.Collections.Generic;
using EffectSystem;
using UnityEngine;

public class EnemyEffectController : AgentEffectController
{
    [SerializeField] private EnemyEffectStateUI _effectUI;
    // 불나방, 

    public override void ApplyEffect(EffectStateTypeEnum type, float duration, int level, float percent = 1f)
    {
        if(!effectDictionary[type].enabled)
            _effectUI.GenerateSlot(type, effectDictionary[type]);
            
        base.ApplyEffect(type, duration, level);
        
    }
}
