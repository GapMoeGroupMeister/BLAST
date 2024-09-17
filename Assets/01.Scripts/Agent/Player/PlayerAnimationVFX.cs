using Crogen.ObjectPooling;
using UnityEngine;

public class PlayerAnimationVFX : MonoBehaviour
{
	[SerializeField] private Transform _stepEffectR;
	[SerializeField] private Transform _stepEffectL;
	[SerializeField] private PoolType _stepEffectPoolType;
	[SerializeField] private Transform _baseTrm;


	public void OnRightStep()
	{
		Vector3 pos = _stepEffectR.position + _baseTrm.forward * 2f;
		pos.y = 0;
		gameObject.Pop(_stepEffectPoolType, pos, Quaternion.identity);
	}

	public void OnLeftStep()
	{
		Vector3 pos = _stepEffectL.position + _baseTrm.forward * 2f;
		pos.y = 0;
		gameObject.Pop(_stepEffectPoolType, pos, Quaternion.identity);
	}
}