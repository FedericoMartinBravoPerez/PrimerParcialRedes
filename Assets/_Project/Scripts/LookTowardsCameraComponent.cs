using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsCameraComponent : MonoBehaviour
{
    [Tooltip("Should the rotation be in world space instead of local?")]
    public bool useWorldRotation = true;


    private void Start()
    {
        if (useWorldRotation)
        {
            // Always face downward in WORLD space
            transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
            
        }
    }

    void LateUpdate()
    {
        if (useWorldRotation)
        {
            // Always face downward in WORLD space
            transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
        }
        else
        {
            // Always face downward relative to parent
            transform.localRotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
        }
    }
}
