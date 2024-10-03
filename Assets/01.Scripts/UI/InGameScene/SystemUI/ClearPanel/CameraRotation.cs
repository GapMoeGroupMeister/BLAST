using System;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private void FixedUpdate()
    {
        Vector3 rotation = new Vector3(0, Time.deltaTime * _rotationSpeed, 0);
        transform.Rotate(rotation);
    }
}