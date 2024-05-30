using System;
using UnityEngine;

public class Agent : MonoBehaviour
{
    #region  Components
    public Animator AnimatorCompo { get; protected set; }
    public MovementController MovementCompo { get; protected set; }
    public Health HealthCompo { get; protected set; }
    #endregion

    public bool CanStateChangeable { get; protected set; } = true;

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        
        MovementCompo = GetComponent<MovementController>();
        HealthCompo = GetComponent<Health>();
    }
    
}