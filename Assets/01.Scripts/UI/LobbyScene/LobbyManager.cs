using UnityEngine;
using UnityEngine.SceneManagement;

namespace  LobbyScene
{
    
    public class LobbyManager : MonoBehaviour
    {
        [SerializeField] private PartChanger _partChanager;
        public void GameStart()
        {
            SaveManager.Instance.SaveData();
            SceneLoadingManager.LoadScene("Vcs_GameScene");
        } 

        public void GameExit()
        {
            Application.Quit();
        }

        public void PartChanger()
        {
        }
    }

}