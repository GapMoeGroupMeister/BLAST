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
    [SerializeField] private PlayerPart _currentPlayerPart;
	[SerializeField] private List<PlayerPart> _playerPartList;

	public PlayerPart GetCurrentPlayerPart() => _currentPlayerPart;

	public PlayerPart Init(PlayerPartType playerPartType)
	{
		if(transform.childCount > 0)
			Destroy(transform.GetChild(0).gameObject);
		
		_currentPlayerPart = Instantiate(_playerPartList.Find(x => x.playerPartType == playerPartType), transform);
		_currentPlayerPart.transform.localPosition = Vector3.zero;
		_currentPlayerPart.transform.localScale = Vector3.one;

		return _currentPlayerPart;
	}
}
