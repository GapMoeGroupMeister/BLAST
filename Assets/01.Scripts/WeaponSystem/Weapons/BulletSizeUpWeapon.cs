using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletSizeUpWeapon : Weapon
{
    public Action<float> ResizeEvent;

	public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            SetMultipliedCounts();
        }	

        return true;
    }

    private void SetMultipliedCounts()
	{
        ResizeEvent?.Invoke(((level / 10f) * 0.55f) + 1f);
	}
}
   