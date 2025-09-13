using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DoNotUse.Example
{
    public class TargetFollow : MonoBehaviour
    {
        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void LateUpdate()
        {
            if (!_target) return;
            transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        }
    }
}
