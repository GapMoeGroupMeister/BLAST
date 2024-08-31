using UnityEngine;

public class LobbySceneUIManager : MonoSingleton<LobbySceneUIManager>
{
    [SerializeField] private SelectDisplayPanel _selectDisplayPanel;

    public void RefreshSelectPartInfo(PlayerPartDataSO data)
    {
        _selectDisplayPanel.SelectPart(data);
    }
}