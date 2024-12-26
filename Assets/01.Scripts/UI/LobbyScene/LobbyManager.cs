using Objects.PartSelect;
using UnityEngine;

namespace  LobbyScene
{
    
    public class LobbyManager : MonoBehaviour
    {
        [SerializeField] private PartChanger _partChanager;
        public void GameStart()
        {
            SaveManager.SaveData();
            SceneLoadingManager.LoadScene("GameScene");
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