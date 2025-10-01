using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerView : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] PlayerController controller;
    [SerializeField] NetworkMecanimAnimator _netAnimator;

    public void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
        controller = GetComponent<PlayerController>();
        _netAnimator = GetComponentInChildren<NetworkMecanimAnimator>();

    }

    public void Update()
    {
        _netAnimator.Animator.SetBool("isMoving", controller.IsMoving);
    }

    public void SetHitTrigger()
    {
        _netAnimator.SetTrigger("isHit");
    }

    public void SetDeathTrigger()
    {
        _netAnimator.SetTrigger("isDeath");
    }
}
