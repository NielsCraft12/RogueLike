using UnityEngine;

public class HealthPickup : BasePickup
{
    [SerializeField]
    private int healAmount = 20;

    public override void OnPickup(GameObject player)
    {
        if (player.TryGetComponent<Health>(out Health health))
        {
            health.Heal(healAmount);
        }
        base.OnPickup(player);
    }
}
