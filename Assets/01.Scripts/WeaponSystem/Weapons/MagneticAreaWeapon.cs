using UnityEngine;

public class MagneticAreaWeapon : Weapon
{
    [SerializeField] private float _randomRange;
    [SerializeField] private float _defaultPower;
    [SerializeField] private float _powerMultiplerByLevel;
    [SerializeField]private MagneticBlock _magneticBlockPrefab;
    private MagneticBlock _magneticBlock;

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            GenerateMagneticBlock();
        }	

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void GenerateMagneticBlock(){

        if(_magneticBlock == null){
            _magneticBlock = Instantiate(_magneticBlockPrefab, transform);
        }
        Vector3 random = Random.insideUnitSphere * _randomRange;
        random.y = 0f;
        Vector3 position = player.transform.position + random;
        _magneticBlock.Active(position);
    }
}
