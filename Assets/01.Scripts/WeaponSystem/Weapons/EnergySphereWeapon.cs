using UnityEngine;

public class EnergySphereWeapon : Weapon
{
    [Header("-----------------------------------------")]
    [SerializeField] private EnergySphere _energySphereEffect;

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            CreateEffect();
        }	

        return true;
    }

    private void CreateEffect()
	{
        Vector3 attackDir = player.currentPlayerPart.GetAttackDir();

        EnergySphere energySphere = Instantiate(_energySphereEffect, 
            player.transform.position + attackDir * 3f + Vector3.up * 3f, 
            Quaternion.LookRotation(attackDir));
        energySphere.Init(level, this);
    }

    protected override void Update()
    {
        base.Update();
    }
}
