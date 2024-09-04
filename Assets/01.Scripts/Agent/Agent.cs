using System;
using UnityEngine;

public class Agent : MonoBehaviour
{
    #region  Components
    public Animator AnimatorCompo { get; protected set; }
    public MovementController MovementCompo { get; protected set; }
    public Health HealthCompo { get; protected set; }
    #endregion
    [field: SerializeField]
    public StatDataSO Stat { get; protected set; }

    public bool CanStateChangeable { get; protected set; } = true;

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        
        MovementCompo = GetComponent<MovementController>();
        HealthCompo = GetComponent<Health>();
        HealthCompo.Initialize(this, Mathf.CeilToInt(Stat.GetValue(StatEnum.MaxHP)));
    }
}