using UnityEngine;

public class CircleRayDamageCaster : DamageCaster
{
	[Header("CircleRayCaster")]
	public Vector3 center;
	public int count;
	[Range(0f, 360f)]
	public float angle;
	[Range(0f, 360f)]
	public float rotation;
	public float radius = 1f;

	public override void CastOverlap()
	{
		Physics.OverlapSphereNonAlloc(center + transform.position, radius, castColliders, _whatIsCastable);
		base.CastOverlap();
	}

	private void OnDrawGizmos()
	{
		if (excluded) Gizmos.color = Color.red;
		else Gizmos.color = Color.green;

		for (int i = 0; i <= count-1; ++i)
		{
			float curAngle = angle * ((float)i/count);

			Gizmos.DrawRay(center + transform.position, Quaternion.Euler(0, curAngle, 0) * Vector3.forward * radius);
		}

		Gizmos.color = Color.white;
	}
}
