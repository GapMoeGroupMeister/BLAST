using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AddListenerOnDieEvent", story: "On Die Send [Event] Message from [Health]", category: "Action", id: "47a76ee34c8d3d2de3d4b46075b1cd96")]
public partial class AddListenerOnDieEventAction : Action
{
    [SerializeReference] public BlackboardVariable<DieEvent> Event;
    [SerializeReference] public BlackboardVariable<Health> Health;

    protected override Status OnStart()
    {
        Health.Value.OnDieEvent.AddListener(HandleOnDieEvent);
        return Status.Success;
    }

    private void HandleOnDieEvent()
    {
        Health.Value.OnDieEvent.RemoveListener(HandleOnDieEvent);
        Event.Value.SendEventMessage();
    }
}

