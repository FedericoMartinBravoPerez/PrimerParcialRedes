using UnityEngine;

public class RapidFireBooster : Booster
{
    public override void PickUp(GameObject picker)
    {
        if (picker.TryGetComponent(out ShootComponent shoot))
        {
            shoot.CooldownBuff(0.1f, 5f);
            Runner.Despawn(Object);
            
        }
    }
}