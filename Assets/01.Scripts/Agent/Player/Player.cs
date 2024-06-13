using System;
using UnityEngine;

public class Player : Agent
{
    public PlayerStateMachine<PlayerStateEnum> StateMachine;
    public PlayerPartController PlayerPartController;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine<PlayerStateEnum>();
        PlayerPartController = PlayerPartController.Instance;

        //임시로 만든거
        PlayerPartController.Init(PlayerPartType.Default);

        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            try
            {
                string typeName = stateEnum.ToString();

                Type t = Type.GetType($"Player{typeName}State");

                StateMachine.AddState(stateEnum, Activator.CreateInstance(t, this, StateMachine, typeName) as PlayerState<PlayerStateEnum>);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        StateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState?.FixedUpdateState();
    }

    private void Update()
    {
        StateMachine.CurrentState?.UpdateState();
    }
}