using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed = 20f;

    [SerializeField]
    private float gravityScale = 1f;

    [SerializeField]
    private float SpriteAngleAjustment = -138f;

    private Rigidbody2D rb;
    private bool isStuck = false;
    private Vector2 direction;

    AttackController attackController;
    private int enemyLayer;

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        // Rotate arrow to face direction immediately
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        attackController = GetComponentInParent<AttackController>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;

        // Set initial velocity based on direction
        rb.linearVelocity = direction * initialSpeed;
    }

    void Update()
    {
        if (!isStuck)
        {
            // Rotate arrow to face its movement direction
            float angle =
                Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg
                + SpriteAngleAjustment;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if (!isStuck)
        // {
        //     isStuck = true;
        //     rb.linearVelocity = Vector2.zero;
        //     rb.bodyType = RigidbodyType2D.Kinematic;

        //     // Ignore collisions with enemy layer after getting stuck
        //     Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, isStuck);

        //     transform.SetParent(collision.transform);
        //     Destroy(gameObject, 3f);
        //     attackController.Hit();
        // }

        attackController.Hit();
        Destroy(gameObject);
    }
}
