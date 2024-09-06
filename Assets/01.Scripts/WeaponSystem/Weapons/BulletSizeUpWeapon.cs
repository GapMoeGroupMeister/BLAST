using UnityEngine;

public class BulletSizeUpWeapon : Weapon
{
    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
        }	

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }
}
