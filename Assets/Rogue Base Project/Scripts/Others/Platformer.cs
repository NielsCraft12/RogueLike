using UnityEngine;

public class Platformer : MonoBehaviour
{
    // We do this so that the player moves with the platform.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
