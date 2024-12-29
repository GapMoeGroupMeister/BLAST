using System.Collections;
using Objects.PartSelect;
using UnityEngine;

namespace LobbyScene
{

    public class LobbyManager : MonoBehaviour
    {
        [SerializeField] private PartChanger _partChanager;
        [SerializeField] private float _sceneTransitionWaitDuration;
        public void GameStart()
        {
            SaveManager.SaveData();
            StartCoroutine(StartCoroutine());
        } 

        private IEnumerator StartCoroutine()
        {
            yield return new WaitForSeconds(_sceneTransitionWaitDuration);
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