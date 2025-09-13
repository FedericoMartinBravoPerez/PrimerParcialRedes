using Fusion;
using Fusion.Addons.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody3D))]

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;

    private NetworkRigidbody3D _netRB;

    public override void Spawned()
    {
        _netRB = GetComponent<NetworkRigidbody3D>();
    }

    public override void FixedUpdateNetwork()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //_netRB.Rigidbody.position += transform.forward * (_speed * Runner.DeltaTime);
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //_netRB.Rigidbody.position -= transform.forward * (_speed * 0.5f * Runner.DeltaTime);
            transform.position -= transform.forward * _speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -_turnSpeed * Time.deltaTime); 
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, _turnSpeed * Time.deltaTime);
        }
    }
}
