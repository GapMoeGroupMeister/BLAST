using Crogen.CrogenPooling;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShootBullet", story: "Shoot [Bullet] to [Target] from [Muzzle]", category: "Action", id: "bd834ee7568018b19ee1537152634736")]
public partial class ShootBulletAction : Action
{
    [SerializeReference] public BlackboardVariable<ProjectilePoolType> Bullet;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Transform> Muzzle;

    protected override Status OnStart()
    {
        Vector3 direction = Target.Value.position - Muzzle.Value.position;
        direction.y = 0;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        Bullet bullet = GameObject.Pop(Bullet.Value, Muzzle.Value.position, Quaternion.Euler(0, angle, 0)) as Bullet;
        return Status.Success;
    }
}

