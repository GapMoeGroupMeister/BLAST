using UnityEngine;

public class CoolingSpeedUpWeapon : Weapon
{
    private MagazineInfo _magazineInfoR;
    private MagazineInfo _magazineInfoL;

    float defaultOverloadDelayR;
    float defaultOverloadDelayL;

    public override void WeaponInit()
    {
        _magazineInfoR = player.currentPlayerPart.magazineInfoR;
        _magazineInfoL = player.currentPlayerPart.magazineInfoL;

        defaultOverloadDelayR = _magazineInfoR.overloadDelay;
        defaultOverloadDelayL = _magazineInfoL.overloadDelay;
    }

    public override bool UseWeapon()
    {
        if(_magazineInfoR is null) return false;

        if(base.UseWeapon())
        {
            _magazineInfoR.overloadDelay = defaultOverloadDelayR - (defaultOverloadDelayR * (level / 20f));
            _magazineInfoL.overloadDelay = defaultOverloadDelayL - (defaultOverloadDelayL * (level / 20f));
        }	

        return true;
    }

	protected override void Update()
    {
        base.Update();
    }
}
