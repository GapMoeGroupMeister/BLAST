using System;
using UnityEngine;

public class Player : Agent
{
    public PlayerStateMachine<PlayerStateEnum> StateMachine;
    public PlayerPart currentPlayerPart;
    [SerializeField] private PlayerPartType _currentPartType;
    private PlayerPartController _playerPartController;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine<PlayerStateEnum>();
        _playerPartController = GetComponent<PlayerPartController>();

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
        currentPlayerPart = _playerPartController.Init(_currentPartType);
    }

    private void Start()
    {

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