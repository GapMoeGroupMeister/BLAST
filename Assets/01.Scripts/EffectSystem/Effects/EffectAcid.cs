namespace EffectSystem
{
    public class EffectAcid : EffectState
    {
        public EffectAcid(Agent agent, bool isResist) : base(agent, isResist)
        {
        }

        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            _owner.HealthCompo.TakeDamage(2 * level);
        }
    }
}