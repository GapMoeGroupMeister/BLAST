using System.Collections;
using Crogen.CrogenPooling;
using UnityEngine;

public class BofUniqueWeapon : UniqueWeapon
{
    [SerializeField] private ProjectilePoolType _ultFireBulletPoolType;
    [SerializeField] private float _duration = 8f;
    private PlayerBOFPart _playerBOFPart;
    
    private ProjectilePoolType _defaultProjectileLPoolType;
    private ProjectilePoolType _defaultProjectileRPoolType;
    
    protected override void Start()
    {
        base.Start();
        if(enabled == false) return;
        _playerBOFPart = PlayerPartController.GetCurrentPlayerPart() as PlayerBOFPart;
        _defaultProjectileLPoolType = _playerBOFPart.magazineInfoL.bulletPoolingType;
        _defaultProjectileRPoolType = _playerBOFPart.magazineInfoR.bulletPoolingType;
    }

    protected override void UseUltWeapon()
    {
        if (UseWeapon() == false) return;
        StartCoroutine(CoroutineUseWeapon());
    }

    private IEnumerator CoroutineUseWeapon()
    {
        _playerBOFPart.magazineInfoL.bulletPoolingType = _ultFireBulletPoolType;
        _playerBOFPart.magazineInfoR.bulletPoolingType = _ultFireBulletPoolType;
        yield return new WaitForSeconds(_duration);
        _playerBOFPart.magazineInfoL.bulletPoolingType = _defaultProjectileLPoolType;
        _playerBOFPart.magazineInfoR.bulletPoolingType = _defaultProjectileRPoolType;
    }
}
