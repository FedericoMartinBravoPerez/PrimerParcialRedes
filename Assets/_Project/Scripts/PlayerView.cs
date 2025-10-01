using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerView : NetworkBehaviour
{

    //[SerializeField] Animator animator;
    [SerializeField] PlayerController controller;
    [SerializeField] NetworkMecanimAnimator _netAnimator;
    //[SerializeField] HealthComponent healthComponent;

    public void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
        controller = GetComponent<PlayerController>();
        _netAnimator = GetComponentInChildren<NetworkMecanimAnimator>();
    }

    public override void FixedUpdateNetwork()
    {
        _netAnimator.Animator.SetBool("isMoving", controller.IsMoving);
    }   //ta rari

    public void SetHitTrigger()
    {
        Debug.Log("is hit");
        _netAnimator.Animator.SetTrigger("isHit"); 
    }

    public void SetDeathTrigger()
    {
        _netAnimator.SetTrigger("isDeath"); 
    }
}
