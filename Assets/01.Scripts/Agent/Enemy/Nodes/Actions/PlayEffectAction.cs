using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Crogen.CrogenPooling;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayEffect", story: "Play [Effect] at [Position] Set Parent [Condition]", category: "Action", id: "3b0049cd976bae11ce909532483178d2")]
public partial class PlayEffectAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Position;
    [SerializeReference] public BlackboardVariable<EffectPoolType> Effect;
    [SerializeReference] public BlackboardVariable<bool> Condition;

    protected override Status OnStart()
    {
        IPoolingObject effect = GameObject.Pop(Effect.Value, Position.Value.position, Quaternion.identity);
        if(Condition.Value)
        {
            effect.gameObject.transform.SetParent(Position.Value);
            effect.gameObject.transform.localPosition = Vector3.zero;
        }
        return Status.Success;
    }
}

