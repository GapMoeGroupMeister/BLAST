using System;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    
    private void Update()
    {
        Vector3 rotation = new Vector3(0, Time.unscaledDeltaTime * _rotationSpeed, 0);
        transform.Rotate(rotation);
    }
}