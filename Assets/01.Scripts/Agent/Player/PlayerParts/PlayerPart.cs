using System;
using UnityEngine;

public class PlayerPart : MonoBehaviour
{
    public PlayerPartType playerPartType;
	private PlayerPartController _playerPartController;

    [SerializeField] private Transform[] _bulletFirePositions;
	[SerializeField] private SkillType[] dedicateSkills;

	[SerializeField] protected float _cooldown = 0.1f;
    protected float _cooldownTimer;

    public event Action<Vector3> OnAttackEvent;

	public LayerMask whatIsEnemy;

	protected virtual void Start()
	{
		_playerPartController = PlayerPartController.Instance;
	}

    protected virtual void Update()
    {
        if (_cooldown > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0)
            {
                _cooldownTimer = 0;
            }
            OnAttackEvent?.Invoke(transform.forward);
        }
    }
}
