using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthComponent : NetworkBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _currentHealth;
    
    public int Current => _currentHealth;
    
    [SerializeField] public UnityEvent OnDeath;
    [SerializeField] public UnityEvent OnHit;
    [SerializeField] public UnityEvent OnHeal;

    private bool _isDead = false;
    public override void Spawned()
    {
        _currentHealth = _maxHealth;
        _isDead = false;
    }
    
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        if (_currentHealth == 0)
        {
            Death();
        }
        else
        {
            OnHit.Invoke();
        }
    }
    
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_Heal(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 1, _maxHealth);
        OnHeal?.Invoke();
    }

    private void Death()
    {
        if (_isDead) return;
        
        _isDead = true;
        GameManager.Instance.RPC_PlayerDefeated(Object.StateAuthority);
        OnDeath.Invoke();
        GetComponent<PlayerController>().enabled = false;
        GetComponent<ShootComponent>().enabled = false;
        Invoke(nameof(Despawn), 3f);
    }

    
    private void Despawn() => Runner.Despawn(Object);
}
