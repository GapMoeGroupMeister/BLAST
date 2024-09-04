using UnityEngine;

namespace EffectSystem
{
    public class EffectBurn : EffectState
    {
        
        public EffectBurn(Agent agent, bool isResist) : base(agent, isResist)
        {
            
        }

        
        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            _ownerHealth.TakeDamage(level);
            PopupTextManager.Instance.GenerateDamagePopup(_ownerTrm.position, level, EffectStateTypeEnum.Burn, true);
        }
    }
}