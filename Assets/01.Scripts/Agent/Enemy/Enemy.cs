using System.Linq;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public abstract class Enemy : Agent, IPoolingObject
{
    public Renderer RendererCompo { get; private set; }
    public EnemyMovement EnemyMovementCompo { get; private set; }

    [Header("Common Setting")]
    public LayerMask whatIsPlayer;

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

    public PoolType OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

    protected override void Awake()
    {
        base.Awake();
        targetTrm = GameManager.Instance.Player.transform;
        HealthCompo.OnDieEvent.AddListener(OnDie);
        colliderCompo = GetComponent<Collider>();
        EnemyMovementCompo = MovementCompo as EnemyMovement;
        EnemyMovementCompo.Initialize(this);
        RendererCompo = transform.Find("Visual/BaseMesh").GetComponent<Renderer>();
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

    public abstract void AnimationEndTrigger(AnimationTriggerEnum triggerBit);

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
        CanStateChangeable = true;
    }

    public virtual void OnPush()
    {
    }
}
