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
    
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private UnityEvent _onHit;

    private bool _isDead = false;
    public override void Spawned()
    {
        _currentHealth = _maxHealth;
    }
    
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        if (_currentHealth == 0)
        {
            Death();
        }
        else
        {
            _onHit.Invoke();
        }
    }
    
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_Heal(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 1, _maxHealth);
    }

    private void Death()
    {
        if (_isDead) return;
        
        _isDead = true;
        GameManager.Instance.RPC_PlayerDefeated(Object.StateAuthority);
        _onDeath.Invoke();
        GetComponent<PlayerController>().enabled = false;
        GetComponent<ShootComponent>().enabled = false;
        Invoke(nameof(Despawn), 3f);
    }

    
    private void Despawn() => Runner.Despawn(Object);
}
