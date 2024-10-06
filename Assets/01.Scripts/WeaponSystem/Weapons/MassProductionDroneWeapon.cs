using UnityEngine;

public class MassProductionDroneWeapon : Weapon
{
    [SerializeField] private MassProductionDrone dronePrefab;

    private int _currentDroneCount = 0;

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            if (level > _currentDroneCount)
			{
                var drone = Instantiate(dronePrefab);
                drone.InitLevel(level);
                ++_currentDroneCount;
			}

        }	

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }
}
