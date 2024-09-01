using System.Linq;
using UnityEngine;

public class LineDamageCaster : DamageCaster
{
	[Header("LineCaster")]
	public Vector3 startPoint;
	public Vector3 endPoint = Vector3.forward;

	public override void CastOverlap()
	{
		//¿À¹ö·¦
		RaycastHit[] hits = new RaycastHit[128];
		Physics.RaycastNonAlloc(transform.position + startPoint, endPoint - startPoint, hits, (endPoint - startPoint).magnitude, _whatIsCastable);

		hits = hits.Where(x => x.transform != null).ToArray();

		for (int i = 0; i < hits.Length; ++i)
			castColliders[i] = hits[i].collider;

		base.CastOverlap();
	}

	private void OnDrawGizmos()
	{
		if (excluded) Gizmos.color = Color.red;
		else Gizmos.color = Color.green;

		Gizmos.DrawLine(startPoint + transform.position, endPoint + transform.position);

		Gizmos.color = Color.white;
	}
}
