using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class Health : MonoBehaviour, IDamageable
{
    public List<Renderer> rendererList;
    [SerializeField] private float _damageDuration = 0.005f;
    public UnityEvent<int, int> OnHealthChangedEvent;
    public UnityEvent OnDieEvent;
    public bool IsInvincibility { get; set; }
    private int _currentHealth = 0;
    public int CurrentHealth => _currentHealth;
    [SerializeField] private int _maxHealth;
    public int MaxHealth => _maxHealth;
    
    [SerializeField] private bool _isDead;
    private Agent _owner;

    private int _damagedID;

	private void Awake()
	{
        _damagedID = Shader.PropertyToID("_IsDamaged");
    }

    public void Initialize(Agent owner, int health)
    {
        _owner = owner;
        _isDead = false;
        _currentHealth = health;
        _maxHealth = health;
    }
    
    public void TakeDamage(int amount)
    {
        if (_isDead) return;
        if (IsInvincibility) return;
        _currentHealth -= amount;
        OnHealthChangedEvent?.Invoke(_currentHealth, _maxHealth);
        StopAllCoroutines();
        foreach (var renderer in rendererList)
        {
            for (int i = 0; i < renderer.materials.Length; ++i)
            {
                renderer.materials[i].SetInt(_damagedID, 0);
            }
        }
        StartCoroutine(CoroutineOnDamaged());
        CheckDie();
    }

    public void RestoreHealth(int amount)
    {
        if (_isDead) return;
        _currentHealth += amount;
        OnHealthChangedEvent?.Invoke(_currentHealth, _maxHealth);
        CheckDie();
    }

    public void CheckDie()
    {
        if (_currentHealth <= 0)
        {
            _isDead = true;
            OnDieEvent?.Invoke();
        }
    }

    [ContextMenu("즉사 디버깅")]
    public void OnDie()
	{
        _currentHealth = 0;
        _isDead = true;
        OnDieEvent?.Invoke();
    }

    private IEnumerator CoroutineOnDamaged()
	{
        foreach (var renderer in rendererList)
        {
            for (int i = 0; i < renderer.materials.Length; ++i)
            {
                renderer.materials[i].SetInt(_damagedID, 1);
            }
        }

        yield return new WaitForSeconds(_damageDuration);

        foreach (var renderer in rendererList)
		{
            for (int i = 0; i < renderer.materials.Length; ++i)
            {
                renderer.materials[i].SetInt(_damagedID, 0);
            }
        }
    }
}