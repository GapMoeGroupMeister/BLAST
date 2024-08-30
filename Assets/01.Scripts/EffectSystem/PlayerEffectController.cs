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

        [ContextMenu("DebugApplyBurn")]
        private void DebugApplyBurnEffect()
        {
            ApplyEffect(EffectStateTypeEnum.Burn, 10f, 1);
        }
        
        public override void ApplyEffect(EffectStateTypeEnum type, float duration, int level)
        {
            if(!effectDictionary[type].enabled)
                _effectPanel.GenerateSlot(effectDictionary[type]);
            
            base.ApplyEffect(type, duration, level);
            
        }
    }
}