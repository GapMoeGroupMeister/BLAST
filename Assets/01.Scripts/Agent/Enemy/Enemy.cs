using System.Linq;
using UnityEngine;
using Crogen.CrogenPooling;
using Unity.Behavior;

public abstract class Enemy : Agent, IPoolingObject
{
    protected BehaviorGraphAgent _btAgent;

    public Renderer RendererCompo { get; private set; }
    public EnemyMovement EnemyMovementCompo { get; private set; }
    public EnemyAnimatorTrigger AnimatorTriggerCompo { get; private set; }

    [Header("Common Setting")]
    public LayerMask whatIsPlayer;
    public LayerMask whatIsObstacle;

    [Header("Attack Setting")]
    public float attackDistance;
    public Transform targetTrm;
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private DamageCaster[] _damageCasters;
    [HideInInspector]
    public Collider colliderCompo;

    public float StunTime { get; protected set; }
    public float Level { get; protected set; }

    protected Collider[] _enemyCheckColliders;

    public string OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

    protected override void Awake()
    {
        base.Awake();
        _btAgent = GetComponent<BehaviorGraphAgent>();
        targetTrm = GameManager.Instance.Player.transform;
        HealthCompo.OnDieEvent.AddListener(OnDie);
        colliderCompo = GetComponent<Collider>();
        EnemyMovementCompo = MovementCompo as EnemyMovement;
        EnemyMovementCompo.Initialize(this);
        Transform visualTrm = transform.Find("Visual");
        AnimatorTriggerCompo = visualTrm.GetComponent<EnemyAnimatorTrigger>();
        RendererCompo = visualTrm.GetComponent<Renderer>();
    }

    public void CastDamage()
    {
        for (int i = 0; i < _damageCasters.Length; ++i)
        {
            _damageCasters[i]?.CastDamage((int)Stat.GetValue(StatEnum.Attack));
        }
    }

    public virtual void OnDie()
    {
        colliderCompo.enabled = false;
        WaveManager.Instance.RemoveEnemy(this);
    }

    public abstract void Stun(float duration);

    public virtual void OnPop()
    {
        colliderCompo.enabled = true;
        Level = WaveManager.Instance.CurrentWave / WaveManager.Instance.stageWaves.wavelist.Length;
        EnemyStatDataSO stat = Stat as EnemyStatDataSO;
        foreach (var key in stat.statModifierDictionary.Keys)
        {
            float currentStat = stat.GetValue(key);
            float statModifier = stat.GetModifierValue(key, Level);
            stat.SetValue(key, currentStat * statModifier);
        }
        HealthCompo.Initialize(this, (int)stat.GetValue(StatEnum.MaxHP));
        HealthCompo.TakeDamage(0);
        EnemyMovementCompo.EnableNavAgent();
        _btAgent.Start();
        CanStateChangeable = true;

    }

    public virtual void OnPush()
    {
        _btAgent.End();
    }

    public BlackboardVariable<T> GetVariable<T>(string variableName)
    {
        if (_btAgent.GetVariable(variableName, out BlackboardVariable<T> variable))
        {
            return variable;
        }
        return null;
    }

    public void RestartBehaviorTree()
    {
        _btAgent.Restart();
    }
}
