using System;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{   
    public class AgentEffectController : MonoBehaviour, IEffectable
    {
        public Dictionary<EffectStateTypeEnum, EffectState> effectDictionary = new Dictionary<EffectStateTypeEnum, EffectState>();
        protected Agent _owner;
        protected float _currentTime = 0f;
        
        protected virtual void Awake()
        {
            _owner = GetComponent<Agent>();
        }

        private void Start()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            foreach (EffectStateTypeEnum effectEnum in Enum.GetValues(typeof(EffectStateTypeEnum)))
            {
                if(effectEnum == 0) continue;
                
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


       
        protected virtual void Update()
        {
            _currentTime += Time.deltaTime;
            bool isOneSecond = _currentTime > 1f;
            foreach (EffectState effect in effectDictionary.Values)
            {
                if (effect.enabled)
                {
                    effect.Update();
                    if (isOneSecond)
                        effect.UpdateBySecond();
                }
            }
            if (isOneSecond)
                _currentTime = 0f;
        }

        
        /**
         * <param name="type">
         * 효과 타입 enum
         * </param>
         * <param name="duration">
         * 효과 지속시간
         * </param>
         * <param name="level">
         * 효과 강도
         * </param>
         * <summary>
         * 효과 부여 메서드
         * </summary>
         */
        public virtual void ApplyEffect(EffectStateTypeEnum type, float duration, int level, float percent = 1f)
        {
            effectDictionary[type].Start(level, duration, percent);
        }

        public virtual void RemoveEffect(EffectStateTypeEnum type)
        {
            effectDictionary[type].Over();
        }
    
        public EffectState GetEffectState(EffectStateTypeEnum type)
        {
            return effectDictionary[type];
        }
        
    }
}