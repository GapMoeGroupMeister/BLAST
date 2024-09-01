namespace EffectSystem
{
    public class EffectBurn : EffectState
    {
        private Health _health;
        
        
        public EffectBurn(Agent agent, bool isResist) : base(agent, isResist)
        {
        }

        
        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            _owner.HealthCompo.TakeDamage(level);
        }
    }
}