using UnityEngine;

public abstract class WeaponEffect : MonoBehaviour
{
	public float range = 1f;
	public Vector3 offset;
	public int attackableCount=1;
	[SerializeField] protected LayerMask _whatIsEnemy;
	protected Collider[] targets;

	protected virtual void OnEnable()
	{
		targets = new Collider[attackableCount];
	}

	protected virtual void FixedUpdate()
	{
		if(Physics.OverlapSphereNonAlloc(transform.position + transform.rotation * offset, range, targets, _whatIsEnemy) > 0)
		{
			OnAttack();
		}
	}

	protected abstract void OnAttack();

	protected void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position + transform.rotation * offset, range);
		Gizmos.color = Color.white;
	}
}
