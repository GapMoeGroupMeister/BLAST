using System;
using UnityEngine;

public class Agent : MonoBehaviour
{

    #region  Components

    
    public MovementController MovementCompo { get; protected set; }
    public Health HealthCompo { get; protected set; }
    

    #endregion


    protected virtual void Awake()
    {
        MovementCompo = GetComponent<MovementController>();
        HealthCompo = GetComponent<Health>();
    }
    
}