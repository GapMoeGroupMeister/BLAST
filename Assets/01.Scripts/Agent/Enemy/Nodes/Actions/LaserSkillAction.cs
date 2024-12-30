using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LaserSkill", story: "Use LaserSkill by [Owner]", category: "Action", id: "1193de303903208bc98e27a26c96619f")]
public partial class LaserSkillAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Owner;

    protected override Status OnStart()
    {
        (Owner.Value as Boss01).EnableLaser();
        return Status.Success;
    }
}

