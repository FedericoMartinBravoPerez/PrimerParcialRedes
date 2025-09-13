using System;
using Fusion;
using UnityEngine;

public class ShootComponent : NetworkBehaviour
{

    [SerializeField] private NetworkPrefabRef _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Runner.Spawn(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
    }
}
