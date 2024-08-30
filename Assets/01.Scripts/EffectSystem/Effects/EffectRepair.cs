namespace EffectSystem
{
    public class EffectRepair : EffectState
    {
        public EffectRepair(Agent agent, bool isResist) : base(agent, isResist)
        {
        }

        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            _owner.HealthCompo.RestoreHealth(level);
        }
    }
}