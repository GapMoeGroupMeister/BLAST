using GameEventSystem;
using LobbyScene;
using UnityEngine;
namespace Objects.PartSelect
{
    public class PartSelectController : MonoBehaviour
    {
        //[SerializeField] private 
        [SerializeField] private PartChanger _partChanger;
        [SerializeField] private GameEventChannelSO _partSelectEventChannel;
        [SerializeField] private SelectDisplayPanel _selectDisplayPanel;

        private void Awake()
        {
            _partSelectEventChannel.AddListener<PlayerPartDataSO>(ChangePart);
            
        }

        public void ChangePart(PlayerPartDataSO dataSO)
        {
            _selectDisplayPanel.ResetGraph();
            _selectDisplayPanel.Open();
            _partChanger.ChangePart(dataSO);
            _selectDisplayPanel.SelectPart(dataSO);
        }

    }
}