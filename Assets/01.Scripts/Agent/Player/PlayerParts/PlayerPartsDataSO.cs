using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PartPair
{
	public int id;
	public string partName;
	public PlayerPart partPrefab;
	public Sprite partImage;
}

[CreateAssetMenu(menuName = "SO/PlayerPartsData")]
public class PlayerPartsDataSO : ScriptableObject
{
	public List<PartPair> partPairList;
}
