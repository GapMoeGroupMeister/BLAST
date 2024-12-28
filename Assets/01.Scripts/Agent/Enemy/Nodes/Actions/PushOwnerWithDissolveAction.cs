using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using DG.Tweening;
using Crogen.CrogenPooling;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PushOwnerWithDissolve", story: "Push [Owner] with Dissolve at [N] Seconds", category: "Action", id: "fcb334561ccf8e3ea9d12ed9219039ac")]
public partial class PushOwnerWithDissolveAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Owner;
    [SerializeReference] public BlackboardVariable<float> N;

    private readonly int _dissolveID = Shader.PropertyToID("_Burned");

    private bool _isComplete = false;

    protected override Status OnStart()
    {
        _isComplete = false;
        Owner.Value.RendererCompo.materials[0].DOFloat(1, _dissolveID, N.Value).OnComplete(() => _isComplete = true);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_isComplete)
        {
            Owner.Value.Push();
            return Status.Success;
        }
        return Status.Running;
    }


}

