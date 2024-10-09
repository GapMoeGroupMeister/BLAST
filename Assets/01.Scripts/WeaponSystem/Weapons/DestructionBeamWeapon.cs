using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionBeamWeapon : Weapon
{
    [SerializeField] private DestructionBeam _destructionBeam;
    [SerializeField] private List<Transform> _beamTrm;
    
    [SerializeField] private float _chargeTime = 1;
    [SerializeField] private float _beamDuration = 1;
    

    private void Start()
    {
        _beamTrm.AddRange(GameManager.Instance.Player.currentPlayerPart.magazineInfoL.bulletFirePositions);
        _beamTrm.AddRange(GameManager.Instance.Player.currentPlayerPart.magazineInfoR.bulletFirePositions);
    }

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            foreach (var trm in _beamTrm)
            {
                DestructionBeam destructionBeam = Instantiate(_destructionBeam, trm.position, trm.rotation);
                destructionBeam.StartBeam(_chargeTime, _beamDuration, trm);
                Destroy(destructionBeam.gameObject, _chargeTime + _beamDuration);
            }
        }	

        return true;
    }
}
