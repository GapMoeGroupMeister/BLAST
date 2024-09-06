using UnityEngine;

public class CircleRayDamageCaster : DamageCaster
{
	[Header("CircleRayCaster (¹Ì¿Ï¼º)")]
	public Vector3 center;
	public uint count;
	[Range(0f, 300f)]
	public float angle;
	[Range(0f, 360f)]
	public float rotation;
	public float radius = 1f;

	public override void CastOverlap()
	{
		Physics.OverlapSphereNonAlloc(center + transform.position, radius, _castColliders, _whatIsCastable);
	}

	private void OnDrawGizmos()
	{
		if (excluded) Gizmos.color = Color.red;
		else Gizmos.color = Color.green;

		if(count==1)
		{
			Gizmos.DrawRay(center + transform.position, Quaternion.Euler(0, 45 - angle*0.5f, 0) * Vector3.forward * radius);
		}
		else
		{
			for (int i = 0; i < count; ++i)
			{
				float curAngle = angle * ((float)i / (count - 1));

				Gizmos.DrawRay(center + transform.position, Quaternion.Euler(0, curAngle - angle*0.5f, 0) * Vector3.forward * radius);
			}
		}
		
		Gizmos.color = Color.white;
	}
}
