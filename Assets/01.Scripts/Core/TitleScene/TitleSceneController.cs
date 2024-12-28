using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
	private bool _isGameStart = false;
	[SerializeField] private string _lobbySceneName;
	private void Update()
	{
		if (_isGameStart == true) return;
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
		{
			_isGameStart = true;
			OnStartGame();
		}
	}

	private void OnStartGame()
	{
		//���߿� Fade ��� �߰�...
		SceneLoadingManager.LoadScene(_lobbySceneName);
	}
}
