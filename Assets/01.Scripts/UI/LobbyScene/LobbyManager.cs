using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{

    public void GameStart()
    {
        SaveManager.Instance.SaveData();
        SceneLoadingManager.LoadScene("Vcs_GameScene");
    } 

    public void GameExit()
	{
        Application.Quit();
	}
}
