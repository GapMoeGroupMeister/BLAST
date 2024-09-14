using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartContrain : MonoBehaviour
{
	[SerializeField] private string _lobbyScene;

    public void OnGameStart()
	{
		SceneLoadingManager.LoadScene(_lobbyScene);
	}
}
