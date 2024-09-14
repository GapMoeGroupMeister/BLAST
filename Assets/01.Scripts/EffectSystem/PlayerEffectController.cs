using UnityEngine;

namespace EffectSystem
{
    public class PlayerEffectController : AgentEffectController
    {
        private Player _player;
        [SerializeField] private EffectStatePanel _effectPanel;
        

        protected override void Awake()
        {
            base.Awake();
            _player = _owner as Player;
        }

        public override void ApplyEffect(EffectStateTypeEnum type, float duration, int level, float percent = 1f)
        {
            if(!effectDictionary[type].enabled)
                _effectPanel.GenerateSlot(type, effectDictionary[type]);
            
            base.ApplyEffect(type, duration, level);
            
        }
    }
}