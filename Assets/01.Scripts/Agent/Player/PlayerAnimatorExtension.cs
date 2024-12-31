using Crogen.CrogenPooling;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimatorExtension : MonoBehaviour
{
    [SerializeField] private UnityEvent _onStepEvent;
    [SerializeField] private Transform _stepEffectR;
    [SerializeField] private Transform _stepEffectL;
    [SerializeField] private Transform _baseTrm;

    public void OnRightStep()
    {
        Vector3 pos = _stepEffectR.position;
        pos.y = 0;
        _onStepEvent?.Invoke();
        gameObject.Pop(EffectPoolType.StepSmokeEffect, pos, Quaternion.identity);
    }

    public void OnLeftStep()
    {
        Vector3 pos = _stepEffectL.position;
        pos.y = 0;
        _onStepEvent?.Invoke();
        gameObject.Pop(EffectPoolType.StepSmokeEffect, pos, Quaternion.identity);
    }}
