using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassProductionDroneAttack : MonoBehaviour
{
	private MassProductionDrone _droneBase;
	[SerializeField] private float _attackRadius = 15f;
	public float attackDelay = 2f;
	public float attackDuration = 0.5f;
	private float _currentAttackTime = 0;
	public int damage = 1;

	[Header("Effect")]
	[SerializeField] private LineRenderer _laserEffect;
	[SerializeField] private DamageCaster _caster;

	private void Awake()
	{
		_droneBase = GetComponent<MassProductionDrone>();
		_laserEffect.gameObject.SetActive(false);
	}

	public void OnAttack(Vector3 targetPos, float duration = 0.3f)
	{
		StartCoroutine(CoroutineOnAttack(targetPos, duration));
	}

	private IEnumerator CoroutineOnAttack(Vector3 targetPos, float duration)
	{
		_laserEffect.SetPosition(0, transform.position);
		_laserEffect.SetPosition(1, targetPos);
		_caster.transform.position = targetPos;
		_laserEffect.gameObject.SetActive(true);
		_caster.CastDamage(damage);
		yield return new WaitForSeconds(duration);
		_laserEffect.gameObject.SetActive(false);
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		UnityEditor.Handles.color = Color.red;
		UnityEditor.Handles.DrawSolidArc(transform.position + Vector3.up * 0.1f, Vector3.up, Vector3.forward, 360f, _attackRadius);
		UnityEditor.Handles.color = Color.white;
	}
#endif
}
