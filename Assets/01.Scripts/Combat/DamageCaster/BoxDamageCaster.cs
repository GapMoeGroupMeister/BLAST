using UnityEngine;

public class BoxDamageCaster : DamageCaster
{
	[Header("BoxCaster")]
	public Vector3 center;
	public Vector3 size = Vector3.one;

	public override void CastOverlap()
	{
		Physics.OverlapBoxNonAlloc(center + transform.position, size * 0.5f, _castColliders, Quaternion.identity, _whatIsCastable);
	}

	private void OnDrawGizmos()
	{
		if (excluded) Gizmos.color = Color.red;
		else Gizmos.color = Color.green;

		Gizmos.DrawWireCube(center + transform.position, size);

		Gizmos.color = Color.white;
	}
}
