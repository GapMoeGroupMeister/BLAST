using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletSizeUpWeapon : Weapon
{
    public List<ISizeupable> sizeupableList;

	private void Start()
	{
        sizeupableList = new List<ISizeupable>();
        sizeupableList = FindExtension.FindInterfaces<ISizeupable>().ToList();
		for (int i = 0; i < sizeupableList.Count; ++i)
		{
            sizeupableList[i].MultipliedCount = 1f;
            sizeupableList[i].DefaultSize = Vector3.one;
            sizeupableList[i].BulletSizeUpWeapon = this;
        }
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

	}

    private void SetMultipliedCounts()
	{
		for (int i = 0; i < sizeupableList.Count; ++i)
		{
            sizeupableList[i].MultipliedCount = ((level / 10f) * 0.55f) + 1f;
        }
	}
}
   