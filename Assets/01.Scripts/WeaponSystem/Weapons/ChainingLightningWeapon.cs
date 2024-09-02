using UnityEngine;

public class ChainingLightningWeapon : Weapon
{
    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            //여기에 로직
        }	

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }
}
