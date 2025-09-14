using UnityEngine;

public class HealthBooster : Booster
{
    public override void PickUp(GameObject picker)
    {
        if (picker.TryGetComponent(out HealthComponent health))
        {
            health.RPC_Heal(3);
            Runner.Despawn(Object);
            
        }
    }
}