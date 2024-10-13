using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletSizeUpWeapon : Weapon
{
    private bool _isFirstUse = true;

    public override bool UseWeapon()
    {
        if (base.UseWeapon())
        {
            if (_isFirstUse)
            {
                ProjectileSizeUpEffect[] effects = FindObjectsOfType<ProjectileSizeUpEffect>(true);
                foreach (var effect in effects)
                {
                    effect.OnEffect(level);
                }
                _isFirstUse = false;
            }
        }

        return true;
    }
}
   