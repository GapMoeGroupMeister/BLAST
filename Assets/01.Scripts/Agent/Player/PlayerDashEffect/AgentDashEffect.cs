using UnityEngine;

public class AgentDashEffect : MonoBehaviour
{
	private MeshFilter _meshFilter;
	[SerializeField] private float _duration = 0.3f;

	public void SetMesh(Mesh mesh)
	{
		_meshFilter ??= transform.Find("Visual").GetComponent<MeshFilter>();
		_meshFilter.mesh = mesh;

		Destroy(gameObject, _duration);
	}
}
