using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum PlayerPartType
{
	Default,
}

public class PlayerPartController : MonoSingleton<PlayerPartController>
{
    public PlayerPart currentPlayerPart;
	[SerializeField] private List<PlayerPart> _playerPartList;

	public void Init(PlayerPartType playerPartType)
	{
		currentPlayerPart = Instantiate(_playerPartList.Find(x => x.playerPartType == playerPartType), transform);
		currentPlayerPart.transform.localPosition = Vector3.zero;
		currentPlayerPart.transform.localScale = Vector3.one;
	}
}
