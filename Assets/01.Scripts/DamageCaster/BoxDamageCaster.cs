using UnityEngine;

public class BoxDamageCaster : DamageCaster
{
	[Header("BoxCaster")]
	public Vector3 center;
	public Vector3 size = Vector3.one;

	private Vector3 GetScaledSize(Vector3 size)
	{
		Vector3 finalVec;
		finalVec.x = size.x * transform.lossyScale.x;
		finalVec.y = size.y * transform.lossyScale.y;
		finalVec.z = size.z * transform.lossyScale.z;

		return finalVec;
	}

	public override void CastOverlap()
	{
		Physics.OverlapBoxNonAlloc(GetScaledCenter(center) + transform.position, GetScaledSize(size) * 0.5f, _castColliders, Quaternion.identity, _whatIsCastable);
	}

	private void OnDrawGizmos()
	{
		if (excluded) Gizmos.color = Color.red;
		else Gizmos.color = Color.green;

		Gizmos.DrawWireCube(GetScaledCenter(center) + transform.position, GetScaledSize(size));

		Gizmos.color = Color.white;
	}
}
