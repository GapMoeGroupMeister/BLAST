using UnityEngine;

public class LaserTailPoint : WeaponEffect
{
    // 따라갈 타겟 오브젝트
    public Transform target;
    [SerializeField] private float _targetDistance;

    private LineRenderer _lineRenderer;
    private BoxDamageCaster _damageCaster;

    private float _posY;

    [SerializeField] private float _attackDelay = 0.1f;
    private float _curAttackDelay = 0f;

	private void Awake()
	{
        Transform visualTrm = transform.Find("Visual");
        Transform damageCasterTrm = transform.Find("DamageCaster");

        _lineRenderer = visualTrm.GetComponent<LineRenderer>();
        _damageCaster = damageCasterTrm.GetComponent<BoxDamageCaster>();

        _posY = visualTrm.localPosition.y;
    }

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
        _damage = 1 + Mathf.RoundToInt(1 * (level / 10f));
    }

    void Update()
    {
        _curAttackDelay += Time.deltaTime;
        if (target == null) return;
        FollowTarget();
    }

    private void FollowTarget()
	{
        Vector3 dir = target.position - transform.position;
        _lineRenderer.SetPosition(1, dir);
        float targetToDistance = Vector3.Distance(target.position, transform.position);

        _damageCaster.size.z = targetToDistance;
        _damageCaster.center.z = targetToDistance / 2;
        _damageCaster.transform.position = transform.position;
        _damageCaster.transform.rotation = Quaternion.LookRotation(dir);
        if(_attackDelay < _curAttackDelay)
		{
            _damageCaster.CastDamage(_damage);
            _curAttackDelay = 0f;
		}

        if (targetToDistance > _targetDistance)
            transform.position = Vector3.MoveTowards(transform.position, target.position, targetToDistance * Time.deltaTime);
    }
}
