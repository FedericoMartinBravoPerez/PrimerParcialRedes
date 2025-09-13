using System;
using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

namespace DoNotUse.Example
{
    [RequireComponent(typeof(NetworkRigidbody3D))]
    public class Player : NetworkBehaviour
    {
        [SerializeField] NetworkPrefabRef _bulletPrefab;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _shootSpawner;

        [SerializeField] private int _initialLife;
        [SerializeField] private int _currentLife;
        private bool _isJumpPressed = false;
        private bool _isShotPressed = false;

        private NetworkRigidbody3D _netRb;


        public override void Spawned()
        {
            _currentLife = _initialLife;

            if (HasStateAuthority)
            {
                var mainCamera = Camera.main;

                if (mainCamera!.TryGetComponent(out TargetFollow follower))
                {
                    follower.SetTarget(transform);
                }
            }

            _netRb = GetComponent<NetworkRigidbody3D>();
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void RPC_TakeDamage(int dmg)
        {
            _currentLife -= dmg;

            if (_currentLife > 0) return;

            Death();
        }

        void Death()
        {
            Runner.Despawn(Object);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJumpPressed = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                
                _isShotPressed = true;
            }
            
            _isShotPressed |= Input.GetKey(KeyCode.E);
            _isJumpPressed |= Input.GetKey(KeyCode.Space);
        }

        public override void FixedUpdateNetwork()
        {
            if (_isJumpPressed)
            {
                Jump();
                _isJumpPressed = false;
            }

            if (_isShotPressed)
            {
                Shot();
                _isShotPressed = false;       
            }
            Move(Input.GetAxis("Horizontal"));        
        }

        private void Move(float xAxi)
        {
            //transform.position += Vector3.right * (xAxi * Runner.DeltaTime * _speed);
            
            //rotation
            if(xAxi != 0)
                transform.right = Vector3.right * Mathf.Sign(xAxi);
            
            // Movimiento
            _netRb.Rigidbody.velocity += Vector3.right * (xAxi * Runner.DeltaTime * _speed * 10);
            
            //Limitacion de velocidad
            if (Mathf.Abs(_netRb.Rigidbody.velocity.x) <= _speed) return;
            
            var velocity = _netRb.Rigidbody.velocity;
            velocity.y = 0;
            velocity = Vector3.ClampMagnitude(velocity, _speed);
            velocity.y = _netRb.Rigidbody.velocity.y;
            
            _netRb.Rigidbody.velocity = velocity;
        }

        void Jump()
        {
            _netRb.Rigidbody.velocity = new Vector3(_netRb.Rigidbody.velocity.x, 0, _netRb.Rigidbody.velocity.z);
            _netRb.Rigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }

        void Shot()
        {
            Runner.Spawn(_bulletPrefab, _shootSpawner.position, _shootSpawner.rotation);
        }
    }
}
