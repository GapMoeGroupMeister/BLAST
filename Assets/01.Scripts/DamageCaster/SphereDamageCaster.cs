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
		Physics.OverlapSphereNonAlloc(GetScaledCenter(center) + transform.position,
			radius * finalRadiusMul, _castColliders, _whatIsCastable);
	}

	private void OnDrawGizmos()
	{
		if (excluded) Gizmos.color = Color.red;
		else Gizmos.color = Color.green;
		float finalRadiusMul = GetScaleMul();
		Vector3 finalCenter;
		finalCenter.x = center.x * transform.lossyScale.x;
		finalCenter.y = center.y * transform.lossyScale.y;
		finalCenter.z = center.z * transform.lossyScale.z;
		Gizmos.DrawWireSphere(finalCenter + transform.position, radius*finalRadiusMul);

		Gizmos.color = Color.white;
	}
}
