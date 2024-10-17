using UnityEngine;

namespace LobbyScene
{
    
    public class LobbySceneUIManager : MonoSingleton<LobbySceneUIManager>
    {
        [SerializeField] private SelectDisplayPanel _selectDisplayPanel;
        public LobbyCameraController camController;

        public void RefreshSelectPartInfo(PlayerPartDataSO data)
        {
            _selectDisplayPanel.SelectPart(data);
        }

        public void HandleMovePartPanel()
        {
            camController.ChangeCamType(LobbyCameraEnum.PartSelect);
        }
    }
}