using UnityEngine;

public class DashExtensionWeapon : Weapon
{
    [SerializeField] private DashExplosionEffect _dashExplosionPrefab;
    PlayerMovement _playerMovement;

	private void Start()
	{
        _playerMovement = GameManager.Instance.Player.MovementCompo as PlayerMovement;
        _playerMovement.OnDashDirectionEvent += OnDash;
    }

    private void OnDash(Vector3 dashDir) => UseWeapon();

	public override bool UseWeapon()
    {
        if (base.UseWeapon())
        {
            DashExplosionEffect effect = 
                Instantiate(_dashExplosionPrefab, 
                _playerMovement.transform.position, 
                Quaternion.identity);
            effect.Init(level, this);
        }	

        return true;
    }
}
