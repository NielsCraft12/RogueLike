using UnityEngine;

public interface IPickupable
{
    void OnPickup(GameObject player);
    void DestroyPickup();
}
