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
        if(base.UseWeapon())
        {
            _magazineInfoR.overloadDelay = defaultOverloadDelayR + (defaultOverloadDelayR * (level / 10f));
            _magazineInfoL.overloadDelay = defaultOverloadDelayL + (defaultOverloadDelayL * (level / 10f));
        }	

        return true;
    }

	protected override void Update()
    {
        base.Update();
    }
}
