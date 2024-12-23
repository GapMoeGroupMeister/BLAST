using System;
using DG.Tweening;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private PlayerPartController _playerPartController;

    private void Start()
    {
        _playerPartController.Init((PlayerPartType)SaveManager.data.partId);
    }

    private void Update()
    {
        Vector3 rotation = new Vector3(0, Time.unscaledDeltaTime * _rotationSpeed, 0);
        transform.DORotate(transform.rotation.eulerAngles + rotation, 0).SetUpdate(true);
    }
}