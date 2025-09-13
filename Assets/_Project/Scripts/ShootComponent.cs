using System;
using Fusion;
using UnityEngine;

public class ShootComponent : NetworkBehaviour
{

    [SerializeField] private NetworkPrefabRef _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _cooldown = 1f;

    private float _cooldownTimer = 0f;
    
    private bool _isShotPressed = false;

    private void Update()
    {
        _isShotPressed = Input.GetKey(KeyCode.E);
        _cooldownTimer += Time.deltaTime;
    }

    public override void FixedUpdateNetwork()
    {
        if (_isShotPressed && _cooldownTimer >= _cooldown)
        {
            _cooldownTimer = 0f;
            Shoot();
            _isShotPressed = false;       
        }
    }

    void Shoot()
    {
        Runner.Spawn(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
    }
}
