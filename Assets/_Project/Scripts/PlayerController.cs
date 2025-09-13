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
            //transform.position += transform.forward * verticalInput * _speed * Runner.DeltaTime;
            _netRB.Rigidbody.velocity += transform.forward * verticalInput * _speed * Runner.DeltaTime;
            if (_netRB.Rigidbody.velocity.sqrMagnitude > _speed * _speed)
                _netRB.Rigidbody.velocity = _netRB.Rigidbody.velocity.normalized * _speed;
            
            _netRB.Rigidbody.AddForce(Vector3.up * verticalInput * _speed * 10, ForceMode.Impulse);
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
