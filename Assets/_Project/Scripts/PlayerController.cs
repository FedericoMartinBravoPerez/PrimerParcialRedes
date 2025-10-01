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
    public float CurrentVelocity => _netRB.Rigidbody.velocity.magnitude;
    public bool IsMoving => _isMoving;
    
    private NetworkRigidbody3D _netRB;
    private bool _isMoving;


    public override void Spawned()
    {
        _netRB = GetComponent<NetworkRigidbody3D>();
        GameManager.Instance.SubscribeToClientsInGame(Object.StateAuthority);

        if (HasStateAuthority)
        {
            
        }
    }

    public override void FixedUpdateNetwork()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Move(float horizontalInput, float verticalInput)
    {

        _isMoving = horizontalInput != 0 || verticalInput != 0;
        
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
    }
}
