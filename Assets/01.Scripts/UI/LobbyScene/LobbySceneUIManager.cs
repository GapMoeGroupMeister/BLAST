using System;
using UnityEngine;

namespace LobbyScene
{
    
    public class LobbySceneUIManager : MonoSingleton<LobbySceneUIManager>
    {
        [SerializeField] private PartChanger _partChanger;
        [SerializeField] private SelectDisplayPanel _selectDisplayPanel;
        [SerializeField] private PartSelectPanel _partSelectPanel;
        [SerializeField] private MenuButtonGroup _menuButtonGroup;
        
        public LobbyCameraController camController;

        // public void RefreshSelectPartInfo(PlayerPartDataSO data)
        // {
        //     _selectDisplayPanel.Open();
        //     _selectDisplayPanel.SelectPart(data);
        // }

        // public void HandleMovePartPanel()
        // {
        //     _partSelectPanel.Open();
        //     camController.ChangeCamType(LobbyCameraEnum.PartSelect);
        // }
        public void HandleMoveMenuPanel()
        {
            _menuButtonGroup.Open();
            camController.ChangeCamType(LobbyCameraEnum.Default);
            _partSelectPanel.Close();

        }

        public void ChangePart(PlayerPartDataSO partSO)
        {
            _partChanger.ChangePart(partSO);
        }
    }
}