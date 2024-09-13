using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    public CinemachineVirtualCamera PlayerCamera => _playerCamera;


}
