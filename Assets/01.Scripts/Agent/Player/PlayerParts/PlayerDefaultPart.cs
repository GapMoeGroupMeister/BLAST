using UnityEngine;

public class PlayerDefaultPart : PlayerPart
{
    [SerializeField] private Transform[] _subAttackPoints;
    public Transform[] GetSubAttackPoints() => _subAttackPoints;
    protected override void OnEnable()
    {
        base.OnEnable();
    } 
}