using System.Collections.Generic;
using UnityEngine;

public enum PlayerPartType
{
	Default,
	Electronic,
	HoleMaker,
	Fire
}

public class PlayerPartController : MonoBehaviour
{
	[SerializeField] private List<PlayerPart> _playerPartList;
	[SerializeField] private Transform _partPoint;
    private static PlayerPart _currentPlayerPart;
	public static PlayerPart GetCurrentPlayerPart() => _currentPlayerPart;

	public PlayerPart Init(PlayerPartType playerPartType)
	{
		_currentPlayerPart = Instantiate(_playerPartList.Find(x => x.playerPartType == playerPartType), _partPoint);
		_currentPlayerPart.transform.localPosition = Vector3.zero;
		_currentPlayerPart.transform.localScale = Vector3.one;

		return _currentPlayerPart;
	}
}
