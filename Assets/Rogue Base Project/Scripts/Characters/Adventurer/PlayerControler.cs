using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    private CharacterInput playerInput;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    private Animator animator;

    private float moveSpeed = 5f;

    [SerializeField]
    public int coins;

    [SerializeField]
    private float maxMoveSpeed = 5f;

    [SerializeField]
    private float crouchSpeed = 2f;

    [SerializeField]
    private int jumpForce = 500;

    private bool isGrounded = true;

    [SerializeField]
    private bool isCrouching = false;

    bool canJump = true;

    int amoutJumpt = 0;

    private BoxCollider2D standingCollider;

    private float ceilingCheckDistance = 0.5f; // Add this field

    private bool crouchButtonHeld = false;

    private AttackController attackController;

    public float hurttimer;

    void Awake()
    {
        standingCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        playerInput = new CharacterInput();
        rb = GetComponent<Rigidbody2D>();
        attackController = GetComponent<AttackController>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Start()
    {
        playerInput.Player.Movement.performed += OnMovement;
        playerInput.Player.Movement.canceled += OnMovement;
        playerInput.Player.Jump.performed += OnJump;
        playerInput.Player.Crouch.performed += OnCrouch;
        playerInput.Player.Crouch.canceled += OnCrouch;
        playerInput.Player.Attack.performed += OnAttack;
    }

    private bool CanStandUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, ceilingCheckDistance);
        return hit.collider == null;
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            crouchButtonHeld = true;
            if (isGrounded)
            {
                isCrouching = true;
            }
        }
        else if (context.canceled)
        {
            crouchButtonHeld = false;
        }
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (amoutJumpt < 2 && canJump)
        {
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            amoutJumpt++;
            animator.SetInteger("Jumps", amoutJumpt);
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (!isCrouching && !attackController.isAttacking)
        {
            attackController.Attack(true);
        }
    }

    private void AttackEnded()
    {
        attackController.StopAttack();
    }

    void move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (moveInput.x > 0)
        {
            if (isGrounded)
            {
                animator.SetFloat("Speed", moveInput.x);
            }
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            if (isGrounded)
            {
                animator.SetFloat("Speed", -moveInput.x);
            }
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            if (isGrounded)
            {
                animator.SetFloat("Speed", 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            amoutJumpt = 0;
            animator.SetInteger("Jumps", amoutJumpt);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IPickupable pickup = other.GetComponent<IPickupable>();
        if (pickup != null)
        {
            pickup.OnPickup(gameObject);
        }
    }

    void FixedUpdate()
    {
        move();

        if (isCrouching || (!CanStandUp() && standingCollider.enabled == false))
        {
            animator.SetBool("IsCrouching", true);
            standingCollider.enabled = false;
            moveSpeed = crouchSpeed;
            canJump = false;
        }
        else
        {
            animator.SetBool("IsCrouching", false);
            standingCollider.enabled = true;
            moveSpeed = maxMoveSpeed;
            canJump = true;
        }

        if (!crouchButtonHeld && CanStandUp())
        {
            isCrouching = false;
        }
    }

    private void Update()
    {
        if (hurttimer > 0)
        {
            hurttimer -= Time.deltaTime;

            if (hurttimer <= 0)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                hurttimer = 0;
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw the ceiling check raycast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * ceilingCheckDistance);
    }
}
