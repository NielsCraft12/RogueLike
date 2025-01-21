using UnityEngine;

public abstract class BasePickup : MonoBehaviour, IPickupable
{
    protected virtual void Start()
    {
        // Initialize pickup
    }

    public virtual void OnPickup(GameObject player)
    {
        // Handle pickup logic
        DestroyPickup();
    }

    public virtual void DestroyPickup()
    {
        Destroy(gameObject);
    }
}
