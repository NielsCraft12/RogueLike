using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Adventurer : MonoBehaviour
{
    [SerializeField]
    private float runSpeed = 40f; // Movement speed.

    [SerializeField]
    private bool doubleJump = true; // Enable for double jump.
    private int maxJumps = 1;
    private int jumps = 0;

    private CharacterController character;
    private AttackController attack;
    private Animator animator;

    private float horizontalMove = 0f; // To what extent it moves horizontally.
    private bool isJumping = false;
    private bool isCrouching = false;

    private int currentDirection = 0; // In which direction it moves.

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction crouchAction;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        attack = GetComponent<AttackController>();
        animator = GetComponent<Animator>();

        // If the double jump is allowed, we increase the maximum of jumps.
        if (doubleJump)
            maxJumps = 2;

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        attackAction = playerInput.actions["Attack"];
        crouchAction = playerInput.actions["Crouch"];
    }

    private void OnEnable()
    {
        jumpAction.performed += ctx => Jump(true);
        jumpAction.canceled += ctx => Jump(false);

        attackAction.performed += ctx => Attack(true);
        attackAction.canceled += ctx => Attack(false);

        crouchAction.performed += ctx => Crouch(true);
        crouchAction.canceled += ctx => Crouch(false);
    }

    private void OnDisable()
    {
        jumpAction.performed -= ctx => Jump(true);
        jumpAction.canceled -= ctx => Jump(false);

        attackAction.performed -= ctx => Attack(true);
        attackAction.canceled -= ctx => Attack(false);

        crouchAction.performed -= ctx => Crouch(true);
        crouchAction.canceled -= ctx => Crouch(false);
    }

    // We get all the inputs.
    private void Update()
    {
        if (currentDirection == 0)
            horizontalMove = moveAction.ReadValue<Vector2>().x * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    private void FixedUpdate()
    {
        // If you are attacking, do not move.
        if (attack.isAttacking)
        {
            character.Move(0, false, false);

            return;
        }

        // Move our character
        character.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
    }

    public void Move(int dir)
    {
        // What is the new direcction.
        currentDirection = dir;

        switch (dir)
        {
            default:
                horizontalMove = 0;
                break;
            case -1:
                horizontalMove = -runSpeed;
                break;
            case 1:
                horizontalMove = runSpeed;
                break;
        }
    }

    public void Jump(bool j)
    {
        // If you want to jump and you have not reached the maximum number of jumps...
        if (j && jumps < maxJumps)
        {
            jumps++;

            // If it is not the first jump.
            if (jumps > 1)
            {
                // Add vertical force again.
                character.Jump();
            }
            else
            {
                // If not, play the jump animation.
                animator.Play("Jump");
            }
        }
        // If you do not want to jump and you're jumping.
        else if (!j && isJumping)
        {
            jumps = 0;
        }

        isJumping = j;

        // The animator is responsible for making the animations depending on the number of jumps.
        animator.SetInteger("Jumps", jumps);
    }

    public void Crouch(bool c)
    {
        // Update the state crouch.
        isCrouching = c;
    }

    public void Attack(bool a)
    {
        // We communicate with the attack controller if we want to attack.
        attack.Attack(a);
    }

    public void OnLanding()
    {
        // When touching the ground, the number of jumps is restored.
        Jump(false);
    }

    public void OnCrouching(bool isCrouching)
    {
        // While we are crouching, the corresponding animation will be played.
        animator.SetBool("IsCrouching", isCrouching);
    }
}
