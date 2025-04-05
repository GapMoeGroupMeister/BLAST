using System;
using Crogen.AttributeExtension;
using UnityEngine;

public class Player : Agent
{
    public PlayerStateMachine<PlayerStateEnum> StateMachine;
    [HideInInspector]public PlayerPart currentPlayerPart;
    public AgentDashEffectCaster playerDashEffectCaster;
    
    [SerializeField] private bool _isDebugMode = false;
    [HideInInspectorByCondition(nameof(_isDebugMode), false)]
    [SerializeField] private PlayerPartType _currentPartType;
    
    private PlayerPartController _playerPartController;
    
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine<PlayerStateEnum>();
        playerDashEffectCaster = GetComponent<AgentDashEffectCaster>();
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
#if UNITY_EDITOR
        
#else
        _isDebugMode=false;
#endif
        
        if(_isDebugMode == false) //디버깅할 때는 인스펙터에서 걍 정해버리기~
            _currentPartType = (PlayerPartType)SaveManager.data.partId;
        currentPlayerPart = _playerPartController.Init(_currentPartType);
        playerDashEffectCaster.meshFilters.Add(currentPlayerPart.GetComponent<MeshFilter>());
        Renderer[] renderers = currentPlayerPart.GetComponentsInChildren<Renderer>();
        HealthCompo.rendererList.AddRange(renderers);
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