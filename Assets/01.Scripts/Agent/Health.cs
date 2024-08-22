using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent<int, int> OnHealthChangedEvent;
    public UnityEvent OnDieEvent;
    private int _currentHealth = 0;
    public int CurrentHealth => _currentHealth;
    private int _maxHealth;
    public int MaxHealth => _maxHealth;
    
    [SerializeField] private bool _isDead;
    private Agent _owner;


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
        _currentHealth -= amount;
        OnHealthChangedEvent?.Invoke(_currentHealth, _maxHealth);
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
}