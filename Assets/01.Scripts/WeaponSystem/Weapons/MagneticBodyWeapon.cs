using UnityEngine;

public class MagneticBodyWeapon : Weapon
{
    [SerializeField] private Transform _playerXPBodyTrm;

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            SetScaleForPlayerXPBody();
        }	

        return true;
    }

    private void SetScaleForPlayerXPBody()
	{
        float scale = 1f + 2 * (level / 10f);

        _playerXPBodyTrm.localScale = new Vector3(scale, 1, scale);
    }

    protected override void Update()
    {
        base.Update();
    }
}
