using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletSizeUpWeapon : Weapon
{
    public List<ISizeupable> sizeupableList;

	private void Awake()
	{
        sizeupableList = new List<ISizeupable>();
    }

	public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            SetMultipliedCounts();
        }	

        return true;
    }

    public void ResetSize(ISizeupable sizeupable)
	{
        sizeupable.MultipliedCount = 1f;
        sizeupable.DefaultSize = Vector3.one;
        sizeupable.BulletSizeUpWeapon = this;
        sizeupableList.Add(sizeupable);
    }

    private void SetMultipliedCounts()
	{
		for (int i = 0; i < sizeupableList.Count; ++i)
		{
            sizeupableList[i].MultipliedCount = ((level / 10f) * 0.55f) + 1f;
        }
	}
}
   