using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShootBulletEffect", story: "Play ShootBullet Skill Effect at [Time] by [Owner]", category: "Action", id: "5f0306bdf047aeb500053c30d03b19de")]
public partial class ShootBulletEffectAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Time;
    [SerializeReference] public BlackboardVariable<Enemy> Owner;

    protected override Status OnStart()
    {
        (Owner.Value as Boss01).ShootBulletEffect(Time.Value);
        return Status.Success;
    }
}

