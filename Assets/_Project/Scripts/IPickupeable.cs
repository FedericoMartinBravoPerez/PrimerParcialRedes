using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public interface IPickupeable
{
    void PickUp(GameObject picker);
}

[RequireComponent(typeof(Collider))]
public abstract class Booster : NetworkBehaviour, IPickupeable
{
    public abstract void PickUp(GameObject picker);
    private void OnTriggerEnter(Collider other)
    {
        PickUp(other.gameObject);
    }
}