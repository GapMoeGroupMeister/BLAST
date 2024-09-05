using Crogen.ObjectPooling;
using UnityEngine;

public class MineWeapon : Weapon
{
    [Header("----------------------------------------")]
    [SerializeField] private PoolType _minePoolType;

	public override void WeaponInit()
	{
		base.WeaponInit();
        (player.MovementCompo as PlayerMovement).OnDistanceTravelledEvent += AutoUseWeaponByValueCheck;
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
        Mine mine = gameObject.Pop(
        _minePoolType, player.transform.position - player.transform.forward
        , Quaternion.identity) as Mine;

        mine.Init(level, this);
    }

    protected override void Update()
    {
        base.Update();
    }
}
