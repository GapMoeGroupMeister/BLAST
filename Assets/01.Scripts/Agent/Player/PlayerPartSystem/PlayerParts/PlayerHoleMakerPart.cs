using UnityEngine;
using DG.Tweening;

public class PlayerHoleMakerPart : PlayerPart
{
	
	protected override void OnEnable()
	{
		base.OnEnable();
		magazineInfoR.OnAttackEvent += AttackPointRotationR;
		magazineInfoL.OnAttackEvent += AttackPointRotationL;
	}

	private void OnDisable()
	{
		magazineInfoR.OnAttackEvent -= AttackPointRotationR;
		magazineInfoL.OnAttackEvent -= AttackPointRotationL;
	}

	private void AttackPointRotationR(Vector3 obj)
	{
		for (int i = 0; i < magazineInfoR.bulletFirePositions.Length; ++i)
		{
			Vector3 originRot = magazineInfoR.bulletFirePositions[i].localEulerAngles + (Vector3.forward * 180);

			magazineInfoR.bulletFirePositions[i].DOLocalRotate(originRot, 0.1f);
		}
	}

	private void AttackPointRotationL(Vector3 obj)
	{
		for (int i = 0; i < magazineInfoL.bulletFirePositions.Length; ++i)
		{
			Vector3 originRot = magazineInfoL.bulletFirePositions[i].localEulerAngles + (Vector3.forward * 180);

			magazineInfoL.bulletFirePositions[i].DOLocalRotate(originRot, 0.1f);
		}
	}
}
