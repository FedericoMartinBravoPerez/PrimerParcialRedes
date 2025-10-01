using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class UIHealth : NetworkBehaviour
{
        private HealthComponent _health;

        [SerializeField] private GameObject[] _hearths;
        
        private void Awake()
        { 
            
            _health = GetComponentInParent<HealthComponent>();
            _health.OnHit.AddListener(UpdateUIHealth);
            _health.OnHeal.AddListener(UpdateUIHealth);
            _health.OnDeath.AddListener(UpdateUIHealth);
        }
    
        private void UpdateUIHealth( )
        {
                for (int i = 0; i < _hearths.Length; i++)
                {
                    _hearths[i].SetActive(i < _health.Current);
                }
        }
        
        private void OnDestroy()
        {
            _health.OnHit.RemoveListener(UpdateUIHealth);
            _health.OnHeal.RemoveListener(UpdateUIHealth);
            _health.OnDeath.RemoveListener(UpdateUIHealth);
        }
}
