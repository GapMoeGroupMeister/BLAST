using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashEffectCaster : MonoBehaviour
{
	public List<MeshFilter> meshFilters;
	public List<SkinnedMeshRenderer> skinnedMeshRenderers;

	[SerializeField] private PlayerDashEffect _playerDashEffectPrefab;

	[ContextMenu("CreateDashEffect")]
	public void CreateDashEffect()
	{
		var dashEffect = Instantiate(_playerDashEffectPrefab);

		dashEffect.SetMesh(GetMesh());
	}

	private Mesh GetMesh()
	{
		Mesh finalMesh = new Mesh();

		CombineInstance[] combine = new CombineInstance[skinnedMeshRenderers.Count + meshFilters.Count];

		for (int i = 0; i < meshFilters.Count; ++i)
		{
			combine[i].mesh = meshFilters[i].mesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
		}

		for (int i = 0; i < skinnedMeshRenderers.Count; ++i)
		{
			Mesh skinnedMesh = Instantiate(skinnedMeshRenderers[i].sharedMesh);
			skinnedMeshRenderers[i].BakeMesh(skinnedMesh);
			combine[i + meshFilters.Count].mesh = skinnedMesh;
			combine[i + meshFilters.Count].transform = skinnedMeshRenderers[i].transform.localToWorldMatrix;
		}

		finalMesh.CombineMeshes(combine);

		return finalMesh;
	}
}
