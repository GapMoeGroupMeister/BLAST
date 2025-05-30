using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
	private bool _isGameStart = false;
	[SerializeField] private string _lobbySceneName;
	public static bool CanMoveToOtherScene { get; set; } = false;

	private void Update()
	{
		if (CanMoveToOtherScene == false) return;
		if (_isGameStart == true) return;
		if(Input.anyKeyDown)
		{
			_isGameStart = true;
			OnStartGame();
		}
	}

	private void OnStartGame()
	{
		//나중에 Fade 기능 추가...
		SceneLoadingManager.LoadScene(_lobbySceneName);
	}
}
