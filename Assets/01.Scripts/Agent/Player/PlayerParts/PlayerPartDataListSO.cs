using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/PartData/PlayerPartDataList")]
public class PlayerPartDataListSO : ScriptableObject
{
	public List<PlayerPartDataSO> partPairList;
}
