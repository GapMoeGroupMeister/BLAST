using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerPartData")]
public class PlayerPartDataSO : ScriptableObject
{
	public int id;
	public string partName;
	public PlayerPart partPrefab;
	public Sprite partImage;

	[Header("Status Setting")] 
	public float damage;
	public float mobility;
	public float defence;
	public float utility;
}

[CreateAssetMenu(menuName = "SO/PlayerPartDataList")]
public class PlayerPartDataListSO : ScriptableObject
{
	public List<PlayerPartDataSO> partPairList;
}
