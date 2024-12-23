using System;
using UnityEngine;

public class SphereDamageCaster : DamageCaster
{
	[Header("SphereCaster")]
	public Vector3 center;
	public float radius = 1f;

	private float GetScaleMul()
	{
		return Mathf.Max(Mathf.Max(transform.lossyScale.x, transform.lossyScale.y), transform.lossyScale.z);
	}

	public override void CastOverlap()
	{
		float finalRadiusMul = GetScaleMul();
		Physics.OverlapSphereNonAlloc(GetFinalCenter(center) + transform.position,
			radius * finalRadiusMul, _castColliders, _whatIsCastable);
	}

	private void OnDrawGizmos()
	{
		if (excluded) Gizmos.color = Color.red;
		else Gizmos.color = Color.green;
		float finalRadiusMul = GetScaleMul();
		Gizmos.DrawWireSphere(GetFinalCenter(center) + transform.position, radius*finalRadiusMul);

		Gizmos.color = Color.white;
	}
}