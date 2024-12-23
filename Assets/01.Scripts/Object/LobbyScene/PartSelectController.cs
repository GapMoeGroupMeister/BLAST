using GameEventSystem;
using UnityEngine;
namespace Objects.PartSelect
{
    public class PartSelectController : MonoBehaviour
    {
        //[SerializeField] private 
        [SerializeField] private TongController _tongController;
        [SerializeField] private GameEventChannelSO _partSelectEventChannel;

        private void Awake()
        {
            _partSelectEventChannel.AddListener<PlayerPartDataSO>(ChangePart);
            
        }

        public void ChangePart(PlayerPartDataSO dataSO)
        {

        }

    }
}