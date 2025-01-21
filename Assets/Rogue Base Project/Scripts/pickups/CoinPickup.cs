using UnityEngine;

public class CoinPickup : BasePickup
{
    [SerializeField]
    private int coinValue = 1;

    public override void OnPickup(GameObject player)
    {
        if (player.GetComponent<PlayerControler>())
        {
            player.GetComponent<PlayerControler>().coins += coinValue;
        }
        base.OnPickup(player);
    }
}
