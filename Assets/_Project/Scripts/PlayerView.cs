using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PlayerController controller;

    public void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<PlayerController>();
    }

    public void Update()
    {
        animator.SetBool("isMoving", controller.CurrentVelocity != 0);
    }
}
