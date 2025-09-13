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
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Move(float horizontalInput, float verticalInput)
    {
        if (verticalInput != 0)
        {
            Vector3 forwardVel = transform.forward * verticalInput * _speed;
            _netRB.Rigidbody.velocity = forwardVel;
        }
        else
        {
            _netRB.Rigidbody.velocity = Vector3.zero;
        }
        
        if(horizontalInput != 0)
            transform.Rotate(Vector3.up, _turnSpeed * horizontalInput * Runner.DeltaTime);
        
        //  if (Input.GetKey(KeyCode.W))
        //  {
        //      //_netRB.Rigidbody.position += transform.forward * (_speed * Runner.DeltaTime);
        //      transform.position += transform.forward * _speed * Time.deltaTime;
        //  }
        //
        //  if (Input.GetKey(KeyCode.S))
        //  {
        //      //_netRB.Rigidbody.position -= transform.forward * (_speed * 0.5f * Runner.DeltaTime);
        //      transform.position -= transform.forward * _speed * Time.deltaTime;
        //  }
        //
        //  if (Input.GetKey(KeyCode.A))
        //  {
        //      transform.Rotate(Vector3.up, -_turnSpeed * Time.deltaTime); 
        //  }
        //
        //  if (Input.GetKey(KeyCode.D))
        //  {
        //      transform.Rotate(Vector3.up, _turnSpeed * Time.deltaTime);
        // }
    }
}
