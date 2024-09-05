using Crogen.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticMissileWeapon : Weapon
{
    [Header("----------------------------------------")]
    [SerializeField] private float _findAroundRound = 20f;
    [SerializeField] private BallisticMissile _ballisticMissilePrefab;
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
        Transform[] enemyPos = GetRoundEffectPos();

        for (int i = 0; i < enemyPos.Length; ++i)
        {
            BallisticMissile ballisticMissileWeapon = 
                Instantiate(_ballisticMissilePrefab, 
                player.transform.position + (Vector3.up * 5), 
                Quaternion.LookRotation(Vector3.up));

            //기달
            yield return new WaitForSeconds(0.2f);

            //타겟 설정(타겟 설정하면 그쪽으로 가면서 죽일 거임)
            ballisticMissileWeapon.SetTarget(enemyPos[i]);
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
        player ??= FindObjectOfType<Player>();
        Gizmos.DrawWireSphere(player.transform.position, _findAroundRound);
	}
}
