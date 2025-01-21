using Sirenix.OdinInspector;
using UnityEngine;

public class WalkToPlayer : Health
{
    [Title("Movement")]
    [SerializeField]
    protected float moveSpeed = 5f;

    [SerializeField]
    protected float minDistance = 3f; // Minimum distance to maintain

    [Title("Attack")]
    [SerializeField]
    protected float attackRange = 1.5f; // Distance at which goblin will attack

    [SerializeField]
    protected float cooldownTime;

    [SerializeField]
    protected float cooldown;

    protected Transform player;
    protected Rigidbody2D rb;
    protected AttackController attackController;

    protected float distanceToPlayer;
    protected Vector2 moveInput;

    public float hurtTimer;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        attackController = GetComponent<AttackController>();
    }

    protected virtual void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (hurtTimer > 0)
        {
            hurtTimer -= Time.deltaTime;

            if (hurtTimer <= 0)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                hurtTimer = 0;
            }
        }
    }

    protected void Movement()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            distanceToPlayer = direction.magnitude;

            // Calculate movement direction
            if (minDistance == 0 || distanceToPlayer >= minDistance)
            {
                moveInput = direction.normalized;
            }
            else
            {
                moveInput = -direction.normalized;
            }

            // Apply movement if not attacking
            if (!animator.GetBool("IsAttacking"))
            {
                rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
            }

            // Handle animation and flipping
            float speedValue = Mathf.Abs(moveInput.x);
            animator.SetFloat("Speed", speedValue);

            if (moveInput.x != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
            }
        }
    }

    protected void Attack()
    {
        bool canAttack =
            distanceToPlayer <= attackRange && distanceToPlayer >= minDistance && cooldown <= 0;

        if (canAttack)
        {
            animator.SetBool("IsAttacking", true);
            attackController.Attack(true);
            animator.SetFloat("Speed", 0);
            rb.linearVelocity = Vector2.zero;
            cooldown = cooldownTime;
        }
        else if (cooldown <= 0)
        {
            attackController.Attack(false);
        }
    }

    // Add this new method to be called from animation event
    protected void OnAttackAnimationEnd()
    {
        animator.SetFloat("Speed", 0);
        animator.SetBool("IsAttacking", false);
    }
}
