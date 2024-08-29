using System;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    
    public class AgentEffectController : MonoBehaviour
    {
        public Dictionary<EffectStateTypeEnum, EffectState> effectDictionary = new Dictionary<EffectStateTypeEnum, EffectState>();
        private Agent _owner;
        private float _currentTime = 0f;
        
        private void Awake()
        {
            Initialize();
            _owner = GetComponent<Agent>();
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            foreach (EffectState effect in effectDictionary.Values)
            {
                if (effect.enabled)
                {
                    effect.Update();
                    if (_currentTime > 1f)
                    {
                        _currentTime = 0f;
                        effect.UpdateBySecond();
                    }    
                }
                
            }

            
        }

        private void Initialize()
        {
            foreach (EffectStateTypeEnum effectEnum in Enum.GetValues(typeof(EffectStateTypeEnum)))
            {
                string typeName = effectEnum.ToString();
                Type t = Type.GetType($"EffectSystem.Effect{typeName}");

                try
                {
                    EffectState effect = Activator.CreateInstance(t, _owner, false) as EffectState;
                    effectDictionary.Add(effectEnum, effect);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Effect Controller : no Effect found [ {typeName} ] - {ex.Message}");
                }
            }
        }

        public void ApplyEffect(EffectStateTypeEnum type, float duration, int level)
        {
            effectDictionary[type].Start(level, duration);
        }
    
    }

}