using System.Collections;
using UnityEngine;

public class PingPongMovement : MonoBehaviour
{
    public Transform target; // Point to which you must go.
    public float speed = 1f;
    public bool flip = false; // Does the object spin when you come back?

    private Vector3 origin; // Point of origin


    private void Awake()
    {
        origin = transform.position;
    }

    private void Start()
    {
        StartCoroutine(Move(target.position));
    }

    private IEnumerator Move(Vector3 point)
    {
        // While he has not arrived...
        while (Vector3.Distance(point, transform.position) > 1)
        {
            // Move to the point.
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);

            yield return null;
        }

        Flip();

        // Repeat the process, in reverse.
        if (point == origin)
        {
            StartCoroutine(Move(target.position));
        }
        else
        {
            StartCoroutine(Move(origin));
        }
    }

    private void Flip()
    {
        if (!flip) return;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }

}
