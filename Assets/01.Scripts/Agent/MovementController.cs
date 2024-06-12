using UnityEngine;

public abstract class MovementController : MonoBehaviour
{
    public abstract void StopImmediately();
    public abstract void SetMovement(Vector3 movement, bool isRotation = false);
}