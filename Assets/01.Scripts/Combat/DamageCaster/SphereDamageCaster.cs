using UnityEngine;

public class SphereDamageCaster : DamageCaster
{
	[Header("SphereCaster")]
	public Vector3 center;
	public float radius = 1f;

	public override void CastOverlap()
	{
		Physics.OverlapSphereNonAlloc(center + transform.position, radius, _castColliders, _whatIsCastable);
	}

	private void OnDrawGizmos()
	{
		if (excluded) Gizmos.color = Color.red;
		else Gizmos.color = Color.green;

		Gizmos.DrawWireSphere(center + transform.position, radius);

		Gizmos.color = Color.white;
	}
}
