using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class HealthComponent : NetworkBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _currentHealth;

    public override void Spawned()
    {
        _currentHealth = _maxHealth;
    }
    
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        if (_currentHealth == 0)
            Death();
    }

    private void Death()
    {
        Runner.Despawn(Object);
    }
}
