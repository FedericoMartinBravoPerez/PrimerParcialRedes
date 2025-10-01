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

    public void Update()
    {
        //Debug.Log("is moving");
        _netAnimator.Animator.SetBool("isMoving", controller.IsMoving);
    }   //solo el cliente lo ve

    public void SetHitTrigger()
    {
        Debug.Log("is hit");
        _netAnimator.SetTrigger("isHit"); //este no esta siendo registrado
    }

    public void SetDeathTrigger()
    {
        //Debug.Log("is dead");
        _netAnimator.SetTrigger("isDeath"); //este anda en ambos lados
    }
}
