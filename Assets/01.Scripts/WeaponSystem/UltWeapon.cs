using System;

public class UltWeapon : Weapon
{
    protected virtual void Start()
    {
        _curCooldown = _cooldown;
        GameManager.Instance.InputReader.UseUltEvent += UseUltWeapon;
    }

    protected void OnDestroy()
    {
        GameManager.Instance.InputReader.UseUltEvent -= UseUltWeapon;
    }

    protected virtual void UseUltWeapon()
    {
        UseWeapon();
    }

    protected override void Update()
    {
        base.Update();
    }
}
