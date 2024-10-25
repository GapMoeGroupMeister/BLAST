using UnityEngine;

namespace LobbyScene
{
    
    public class LobbySceneUIManager : MonoSingleton<LobbySceneUIManager>
    {
        [SerializeField] private SelectDisplayPanel _selectDisplayPanel;
        [SerializeField] private PartSelectPanel _partSelectPanel;
        [SerializeField] private MenuButtonGroup _menuButtonGroup;
        public LobbyCameraController camController;

        public void RefreshSelectPartInfo(PlayerPartDataSO data)
        {
            _selectDisplayPanel.Open();
            _selectDisplayPanel.SelectPart(data);
        }

        public void HandleMovePartPanel()
        {
            camController.ChangeCamType(LobbyCameraEnum.PartSelect);
        }
        public void HandleMoveMenuPanel()
        {
            _menuButtonGroup.Close();
            camController.ChangeCamType(LobbyCameraEnum.Default);
        }
    }
}