using Crogen.CrogenPooling;
using UnityEngine;

public class MineWeapon : Weapon
{
    [Header("----------------------------------------")]
    //[SerializeField] private PoolType _minePoolType;
    [SerializeField] private float _distance = 10f;
    private int _distanceCount = 1;
	public override void WeaponInit()
	{
		base.WeaponInit();
        WeaponConditionalEventManager.AddListener("MineWeapon", HandleUseWeaponByTravelled);
    }

    private void HandleUseWeaponByTravelled(object distance)
    {
        if (((float)distance / _distance) > _distanceCount)
        {
            Debug.Log("dfdf");
            _distanceCount++;
            UseWeapon();
        }
    }
    
    public override bool UseWeapon()
    {
        if (base.UseWeapon())
        {
            CreateMine();
        }

        return true;
    }

    private void CreateMine()
	{
        // Mine mine = gameObject.Pop(
        // _minePoolType, player.transform.position - player.transform.forward
        // , Quaternion.identity) as Mine;
        //
        // mine.Init(level, this);
    }

    protected override void Update()
    {
        base.Update();
    }
}
