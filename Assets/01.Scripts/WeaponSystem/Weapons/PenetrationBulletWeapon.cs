using UnityEngine;

public class PenetrationBulletWeapon : Weapon
{
    private bool _isFirstUse = true;

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            if(_isFirstUse)
			{
                ProjectilePenetrationEffect[] effects = FindObjectsOfType<ProjectilePenetrationEffect>(true);
				foreach (var effect in effects)
				{
                    effect.OnEffect(level);
                }
                _isFirstUse = false;
			}
        }	

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }
}