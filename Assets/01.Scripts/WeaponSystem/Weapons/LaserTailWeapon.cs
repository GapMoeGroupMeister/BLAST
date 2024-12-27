using System.Collections.Generic;
using UnityEngine;

public class LaserTailWeapon : Weapon
{
    [SerializeField] private LaserTailPoint _laserTailPointPrefab;
    private List<Transform> _pointTargets;
    private int _currentDroneCount = 0;

	private void Start()
	{
        _pointTargets = new List<Transform>();
        _pointTargets.Add(GameManager.Instance.Player.transform);
    }

	public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            if (level > _currentDroneCount)
            {
                var tailPoint = Instantiate(_laserTailPointPrefab);
                tailPoint.target = _pointTargets[(int)level - 1];
                _pointTargets.Add(tailPoint.transform);
                tailPoint.Init(level, this);
                ++_currentDroneCount;
            }
        }	

        return true;
    }
}
