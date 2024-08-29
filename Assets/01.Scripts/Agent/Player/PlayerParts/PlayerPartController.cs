using System.Collections.Generic;
using UnityEngine;

public enum PlayerPartType
{
	Default,
	Electronic,
	HoleMaker,
	Fire
}

public class PlayerPartController : MonoSingleton<PlayerPartController>
{
    public PlayerPart currentPlayerPart;
	[SerializeField] private List<PlayerPart> _playerPartList;

	public void Init(PlayerPartType playerPartType)
	{
		if(transform.childCount > 0)
			Destroy(transform.GetChild(0).gameObject);
		
		currentPlayerPart = Instantiate(_playerPartList.Find(x => x.playerPartType == playerPartType), transform);
		currentPlayerPart.transform.localPosition = Vector3.zero;
		currentPlayerPart.transform.localScale = Vector3.one;
	}
}
