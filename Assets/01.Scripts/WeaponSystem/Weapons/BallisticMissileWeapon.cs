using Crogen.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticMissileWeapon : Weapon
{
    [SerializeField] private float _findAroundRound = 20f;
    [SerializeField] private PoolType _ballisticMissilePoolType;
    private PlayerDefaultPart _basePart;

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            _basePart ??= (player.currentPlayerPart as PlayerDefaultPart);
            FireBallisticMissile();
        }	

        return true;
    }

    //주위의 적들 위치 가져오고
    private Transform[] GetRoundEffectPos()
	{
        Collider[] colliders = new Collider[level*2];
        int count = Physics.OverlapSphereNonAlloc(player.transform.position, _findAroundRound, colliders, whatIsEnemy);

        List<Transform> positions = new List<Transform>();

		for (int i = 0; i < count; ++i)
		{
            positions.Add(colliders[i].transform);
        }

        return positions.ToArray();

    }

    //쏜다
    public void FireBallisticMissile()
    {
        StartCoroutine(CoroutineFireBallisticMissile());
    }

    private IEnumerator CoroutineFireBallisticMissile()
	{
        Transform[] firePos = _basePart.GetSubAttackPoints();
        Transform[] enemyPos = GetRoundEffectPos();

        for (int i = 0; i < enemyPos.Length; ++i)
        {
            //타겟
            Transform targetTrm = firePos[i % 2];

            BallisticMissile ballisticMissile =
                gameObject.Pop(_ballisticMissilePoolType,
                targetTrm.position, //번갈아가며 발싸 (개수 맞추기 위한)
                Quaternion.identity) as BallisticMissile;

            //기달
            yield return new WaitForSeconds(0.2f);

            //타겟 설정(타겟 설정하면 그쪽으로 가면서 죽일 거임)
            ballisticMissile.SetTarget(targetTrm);
        }
    }

	private void OnDestroy()
	{
        StopAllCoroutines();
	}

	protected override void Update()
    {
        base.Update();
    }

	private void OnDrawGizmosSelected()
	{
        Gizmos.DrawWireSphere(transform.position, _findAroundRound);
	}
}
