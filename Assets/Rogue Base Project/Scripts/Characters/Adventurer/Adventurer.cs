using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Adventurer : MonoBehaviour
{
    [SerializeField] private float runSpeed = 40f; // Movement speed.

    [SerializeField] private bool doubleJump = true; // Enable for double jump.
    private int maxJumps = 1;
    private int jumps = 0;


    private CharacterController character;
    private AttackController attack;
    private Animator animator;

    private float horizontalMove = 0f; // To what extent it moves horizontally.
    private bool isJumping = false;
    private bool isCrouching = false;

    private int currentDirection = 0; // In which direction it moves.


    private void Awake()
    {
        character = GetComponent<CharacterController>();
        attack = GetComponent<AttackController>();
        animator = GetComponent<Animator>();

        // If the double jump is allowed, we increase the maximum of jumps.
        if (doubleJump) maxJumps = 2;
    }

    // We get all the inputs.
    private void Update()
    {
        if(currentDirection == 0)
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            Jump(true);
        }

        if (Input.GetButtonDown("Attack") && !isJumping)
        {
            Attack(true);
        }
        else if (Input.GetButtonUp("Attack"))
        {
            Attack(false);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            Crouch(true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Crouch(false);
        }
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
            default: horizontalMove = 0; break;
            case -1: horizontalMove = -runSpeed; break;
            case 1: horizontalMove = runSpeed; break;
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
