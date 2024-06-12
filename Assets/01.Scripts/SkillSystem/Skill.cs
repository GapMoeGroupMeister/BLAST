using UnityEngine;

//스킬을 UI하고 연동해서 스킬 쿨타임을 보여주기 위해서
public delegate void CooldownInfoEvent(float current, float total);

public class Skill : MonoBehaviour
{
    public bool skillEnabled;
    [SerializeField] protected bool _isPassiveSkill;
    [SerializeField] protected float _cooldown;

    [HideInInspector] public Player player;
    protected float _cooldownTimer;

    public event CooldownInfoEvent OnCooldownEvent;

    public LayerMask whatIsEnemy;

    protected virtual void Start()
    {
        player = GameManager.Instance.Player;
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
            OnCooldownEvent?.Invoke(_cooldownTimer, _cooldown);
        }
    }

    public virtual bool UseSkill()
    {
        if (_cooldownTimer > 0 || skillEnabled == false) return false;

        _cooldownTimer = _cooldown;
        return true;
    }

    public void UnlockSkill()
    {
        skillEnabled = true;
        if (_isPassiveSkill)
        {
            SkillManager.Instance.AddPassiveSkill(this);
        }
    }
}