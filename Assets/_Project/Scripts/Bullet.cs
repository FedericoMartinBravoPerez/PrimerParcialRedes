using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private NetworkRigidbody3D _netRb;
    [SerializeField] float _force = 100f;
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private int _dmg = 10;
    
    private TickTimer _timer;

    // al ser spawneada, agregarle al rigidbody un addforce en su direcciont .right
    public override void Spawned()
    {
        ExecuteForce();

        _timer = TickTimer.CreateFromSeconds(Runner, _lifeTime);
    }
    void ExecuteForce()
    {
        _netRb.Rigidbody.AddForce(transform.right * _force, ForceMode.VelocityChange);
    }
    
    // al colisionar con un jugador, restartle vida en base al damage

    public override void FixedUpdateNetwork()
    {
        
        if (_timer.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!HasStateAuthority) return;
        
        if (other.TryGetComponent(out Player enemy))
        {
            enemy.RPC_TakeDamage(_dmg);
        }
        
        Runner.Despawn(Object);
    }

}
