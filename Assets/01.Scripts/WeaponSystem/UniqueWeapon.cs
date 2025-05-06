using System;

public class UniqueWeapon : Weapon
{
    protected virtual void Start()
    {
        if (PlayerPartController.GetCurrentPlayerPart().playerPartType != partType)
        {
            enabled = false;
            return;
        }
        _curCooldown = _cooldown;
        GameManager.Instance.InputReader.UseUltEvent += UseUltWeapon;
    }

    protected virtual void UseUltWeapon()
    {
    }

    protected override void Update()
    {
        base.Update();
    }
}
