using UnityEngine;

public class StunBulletWeapon : Weapon
{
    private bool _isFirstUse = true;

    public override bool UseWeapon()
    {
        if (base.UseWeapon())
        {
            if (_isFirstUse)
            {
                ProjectileStunEffect[] effects = FindObjectsOfType<ProjectileStunEffect>(true);
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
