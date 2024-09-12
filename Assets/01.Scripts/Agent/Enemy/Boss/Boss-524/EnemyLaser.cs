using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public void EnableLaser(float length)
    {
        transform.localScale = new Vector3(1, 1, length / 8);
        transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.forward * (length / 8), 0.5f);
    }
}
