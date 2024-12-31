using System;
using Objects.PartSelect;
using UnityEngine;

namespace LobbyScene
{
    
    public class LobbySceneUIManager : MonoBehaviour
    {
        [SerializeField] private PartSelectPanel _partSelectPanel;
        [SerializeField] private MenuButtonGroup _menuButtonGroup;
        
        public LobbyCameraController camController;

        // public void RefreshSelectPartInfo(PlayerPartDataSO data)
        // {
        //     _selectDisplayPanel.Open();
        //     _selectDisplayPanel.SelectPart(data);
        // }

        public void HandleMovePartPanel()
        {
            _partSelectPanel.Open();
            camController.ChangeCamType(LobbyCameraEnum.PartSelect);
        }
        public void HandleMoveMenuPanel()
        {
            _menuButtonGroup.Open();
            camController.ChangeCamType(LobbyCameraEnum.Default);
            _partSelectPanel.Close();

        }

    }
}