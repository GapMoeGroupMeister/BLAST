using System;
using UnityEngine;

public class NewClearWeapon : Weapon
{
    [SerializeField] private Nuclear _nuclear;

    private void Awake()
    {
        _nuclear = Instantiate(_nuclear);
    }

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            _nuclear.SetPos(player.transform.position);
            _nuclear.ExplodeNuclear();
            
        }	

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }
}
