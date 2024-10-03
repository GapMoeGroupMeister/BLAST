using Crogen.ObjectPooling;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationVFX : MonoBehaviour
{
	[SerializeField] private UnityEvent _onStepEvent;
	[SerializeField] private Transform _stepEffectR;
	[SerializeField] private Transform _stepEffectL;
	[SerializeField] private PoolType _stepEffectPoolType;
	[SerializeField] private Transform _baseTrm;


	public void OnRightStep()
	{
		Vector3 pos = _stepEffectR.position + _baseTrm.forward * 2f;
		pos.y = 0;
		_onStepEvent?.Invoke();
		gameObject.Pop(_stepEffectPoolType, pos, Quaternion.identity);
	}

	public void OnLeftStep()
	{
		Vector3 pos = _stepEffectL.position + _baseTrm.forward * 2f;
		pos.y = 0;
		_onStepEvent?.Invoke();
		gameObject.Pop(_stepEffectPoolType, pos, Quaternion.identity);
	}
}