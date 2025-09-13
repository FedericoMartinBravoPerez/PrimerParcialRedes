using System;
using Fusion;
using UnityEngine;

public class ShootComponent : NetworkBehaviour
{

    [SerializeField] private NetworkPrefabRef _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _cooldown = 1f;

    private float _cooldownTimer = 0f;
    private bool _
    
    private bool _isShotPressed = false;

    private void Update()
    {
        _isShotPressed |= Input.GetKeyDown(KeyCode.E);
    }

    public override void FixedUpdateNetwork()
    {
        if (_isShotPressed)
        {
            Shoot();
            _isShotPressed = false;       
        }
    }

    void Shoot()
    {
        Runner.Spawn(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
    }
}
