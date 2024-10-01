using UnityEngine;

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
    public CapsuleCollider capsuleCollider;

    public float StunTime { get; protected set; }

    protected Collider[] _enemyCheckColliders;

    public PoolType OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

    private void OnValidate()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null) targetTrm = player.transform;

    }

    protected override void Awake()
    {
        base.Awake();
        HealthCompo.OnDieEvent.AddListener(OnDie);
        capsuleCollider = GetComponent<CapsuleCollider>();
        EnemyMovementCompo = MovementCompo as EnemyMovement;
        EnemyMovementCompo.Initialize(this);
        RendererCompo = transform.Find("Visual/BaseMesh").GetComponent<Renderer>();
    }

    public void CastDamage()
	{
		for (int i = 0; i < _damageCasters.Length; ++i)
		{
            _damageCasters[i]?.CastDamage(_damageAmount);
		}
	}

    public abstract void OnDie();

    public abstract void AnimationEndTrigger(AnimationTriggerEnum triggerBit);

    public abstract void Stun(float duration);

    public virtual void OnPop()
    {
        targetTrm  = GameManager.Instance.Player.transform;
        CanStateChangeable = true;
    }

    public virtual void OnPush()
    {
    }
}
