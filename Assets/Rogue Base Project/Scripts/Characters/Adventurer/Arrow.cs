using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 20f;
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float SpriteAngleAjustment = -138f;

    private Rigidbody2D rb;
    private bool isStuck = false;
    private int direction = 1; // 1 for right, -1 for left

    public void Initialize(bool facingRight)
    {
        direction = facingRight ? 1 : -1;

        // Flip the sprite if facing left
        if (!facingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;

        // Initialize arrow velocity using the direction
        rb.linearVelocity = (transform.right * direction) * initialSpeed;
    }

    void Update()
    {
        if (!isStuck)
        {
            // Rotate arrow to face its movement direction
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg + SpriteAngleAjustment;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isStuck)
        {
            isStuck = true;
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;

            // Optional: Parent the arrow to the object it hit
            transform.SetParent(collision.transform);
        }
    }
}
