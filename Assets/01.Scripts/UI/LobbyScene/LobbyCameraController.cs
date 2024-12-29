using Cinemachine;
using UnityEngine;

namespace LobbyScene
{

    public enum LobbyCameraEnum
    {
        Default = 0,
        PartSelect,
        Waiting
    }
    public class LobbyCameraController: MonoBehaviour
    {
        public CinemachineBlendListCamera blendListCam;
        private CinemachineVirtualCameraBase[] _blendCams;

        private void Start()
        {
            _blendCams = blendListCam.ChildCameras;
        }

        public void ChangeCamType(LobbyCameraEnum camType)
        {
            blendListCam.m_Instructions[0].m_VirtualCamera = _blendCams[(int)camType];
        }
    }
}