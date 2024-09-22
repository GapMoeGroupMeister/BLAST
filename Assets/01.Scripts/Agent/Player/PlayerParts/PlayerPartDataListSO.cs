using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/PartData/PlayerPartDataList")]
public class PlayerPartDataListSO : ScriptableObject
{
	public List<PlayerPartDataSO> partPairList;

	public PlayerPartDataSO GetData(int id)
	{
		for (int i = 0; i < partPairList.Count; i++)
		{
			if(partPairList[i].id == id){
				return partPairList[i];
			}
		}
		Debug.Log($"Not Exist part ID {id}");
		return null;
	}
}
