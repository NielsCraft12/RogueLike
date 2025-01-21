using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int damage = 10; // Amount of damage.
    public float speed = 1f; // Attack speed.
    public float range = 1f; // Attack radius that takes as its center the attackPoint.
    public Transform attackPoint; // Point from which to attack.
    public LayerMask whatIsEnemy; // A mask determining what is enemy to the character.

    [HideInInspector]
    public bool isAttacking = false;

    private Animator animator;
    private bool canAttack = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack(bool a)
    {
        if (!canAttack)
            return;

        if (a && !isAttacking)
        {
            isAttacking = true;
            canAttack = false;
            animator.SetFloat("AttackSpeed", speed);
            animator.SetBool("IsAttacking", true);
        }
    }

    public void Hit()
    {
        // Get a list with all enemies within range.
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(
            attackPoint.position,
            range,
            whatIsEnemy
        );

        // It damages the enemies of the list.
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].GetComponent<WalkToPlayer>())
            {
                enemiesToDamage[i].GetComponent<WalkToPlayer>().hurtTimer = 0.1f;
            }
            else if (enemiesToDamage[i].GetComponent<PlayerControler>())
            {
                enemiesToDamage[i].GetComponent<PlayerControler>().hurttimer = 0.1f;
            }
            enemiesToDamage[i].GetComponent<Renderer>().material.color = Color.red;
            enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
        }
    }

    public void StopAttack()
    {
        isAttacking = false;
        canAttack = true;
        animator.SetBool("IsAttacking", false);
    }

    // Draw the attack area.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
