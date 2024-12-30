using System.Collections;
using Objects.PartSelect;
using UnityEngine;

namespace LobbyScene
{

    public class LobbyManager : MonoBehaviour
    {
        [SerializeField] private PartChanger _partChanager;
        [SerializeField] private float _sceneTransitionWaitDuration;
        public void GameStart(string sceneName)
        {
            SaveManager.SaveData();
            StartCoroutine(GameStartCoroutine(sceneName));
        } 

        private IEnumerator GameStartCoroutine(string sceneName)
        {
            yield return new WaitForSeconds(_sceneTransitionWaitDuration);
            SceneLoadingManager.LoadScene(sceneName);
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