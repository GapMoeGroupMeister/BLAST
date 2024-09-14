using System;

namespace EffectSystem
{
    [Flags]
    public enum EffectStateTypeEnum
    {
        None = 0,
        Burn = 1,
        Shock = 2,
        Acid = 4,
        Repair = 8,
        Stun = 16
    }
}