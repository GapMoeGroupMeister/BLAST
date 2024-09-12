using System.Collections.Generic;
using UnityEngine;

public class NapalmWeapon : Weapon
{
    [SerializeField] private Napalm _napalmPrefab;
    private List<Napalm> _napalms;
    
    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            //여기에 로직
        }	

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }
}
