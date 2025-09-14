using System;
using System.Collections;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class ShootComponent : NetworkBehaviour
{

    [SerializeField] private NetworkPrefabRef _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _cooldown = 1f;
    
    private float _currentCooldown = 1f;

    private float _cooldownTimer = 0f;
    private bool _isShotPressed = false;


    public override void Spawned()
    {
        _currentCooldown = _cooldown;
    }
    private void Update()
    {
        _isShotPressed = Input.GetKey(KeyCode.E);
    }

    public override void FixedUpdateNetwork()
    {
        _cooldownTimer += Runner.DeltaTime;
        
        if (_isShotPressed && _cooldownTimer >= _currentCooldown)
        {
            _cooldownTimer = 0f;
            Shoot();
            _isShotPressed = false;       
        }
    }
    
    public void CooldownBuff(float ratio, float time)
    {
        StartCoroutine(CooldownBuffCoroutine(ratio, time));
    }


    private IEnumerator CooldownBuffCoroutine(float ratio, float duration)
    {
        _currentCooldown *= ratio;
        if (_currentCooldown < 0f) _currentCooldown = 0f;
        yield return new WaitForSeconds(duration);
        _currentCooldown = _cooldown;
    }
    void Shoot()
    {
        Runner.Spawn(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
    }
}
