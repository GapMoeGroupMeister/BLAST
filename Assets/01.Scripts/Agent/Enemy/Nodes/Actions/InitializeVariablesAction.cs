using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "InitializeVariables", story: "Initialize Variables From [Self]", category: "Action", id: "d041e6a90b4119190b17d99ee71897e5")]
public partial class InitializeVariablesAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;

    protected override Status OnStart()
    {
        Self.Value.GetVariable<Enemy>("Owner").Value = Self.Value;
        Self.Value.GetVariable<Transform>("Target").Value = Self.Value.targetTrm;
        Self.Value.GetVariable<Health>("Health").Value = Self.Value.HealthCompo;
        Self.Value.GetVariable<EnemyMovement>("Movement").Value = Self.Value.MovementCompo as EnemyMovement;
        Self.Value.GetVariable<Animator>("Animator").Value = Self.Value.AnimatorCompo;
        Self.Value.GetVariable<EnemyAnimatorTrigger>("AnimatorTrigger").Value = Self.Value.AnimatorTriggerCompo;
        return Status.Success;
    }
}

