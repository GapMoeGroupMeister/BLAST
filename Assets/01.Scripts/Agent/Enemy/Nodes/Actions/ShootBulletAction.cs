using Crogen.CrogenPooling;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShootBullet", story: "Shoot [Bullet] from [Muzzle]", category: "Action", id: "bd834ee7568018b19ee1537152634736")]
public partial class ShootBulletAction : Action
{
    [SerializeReference] public BlackboardVariable<ProjectilePoolType> Bullet;
    [SerializeReference] public BlackboardVariable<Transform> Muzzle;

    protected override Status OnStart()
    {
        Bullet bullet = GameObject.Pop(Bullet.Value, Muzzle.Value.position, Quaternion.LookRotation(Muzzle.Value.forward)) as Bullet;
        return Status.Success;
    }
}

