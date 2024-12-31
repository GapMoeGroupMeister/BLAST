using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetConditRotation", story: "Set [Transform] Local [IsLocal] Rotation to [Destination] at [Duration]", category: "Action", id: "5f873f35e5aa68a9734948f0e49a5154")]
public partial class SetConditRotationAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Transform;
    [SerializeReference] public BlackboardVariable<bool> IsLocal;
    [SerializeReference] public BlackboardVariable<Vector3> Destination;
    [SerializeReference] public BlackboardVariable<float> Duration;

    private float _progress = 0;
    private Quaternion _startRotation;
    private Quaternion _endRotation;

    protected override Status OnStart()
    {
        if (Duration.Value == 0)
        {
            if (IsLocal.Value)
                Transform.Value.localRotation = Quaternion.Euler(Destination.Value);
            else
                Transform.Value.rotation = Quaternion.Euler(Destination.Value);
            return Status.Success;
        }
        _startRotation = IsLocal.Value ? Transform.Value.localRotation : Transform.Value.rotation;
        _endRotation = Quaternion.Euler(Destination.Value);
        _progress = 0;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _progress += Time.deltaTime / Duration.Value;
        _progress = Mathf.Clamp01(_progress);
        if (IsLocal.Value)
            Transform.Value.localRotation = Quaternion.Lerp(_startRotation, _endRotation, _progress);
        else
            Transform.Value.rotation = Quaternion.Lerp(_startRotation, _endRotation, _progress);
        if(_progress >= 1)
            return Status.Success;
        return Status.Running;
    }
}

