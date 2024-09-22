using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{

    public void GameStart()
    {
        SaveManager.Instance.SaveData();
        SceneManager.LoadScene("GameScene");
    } 

}
