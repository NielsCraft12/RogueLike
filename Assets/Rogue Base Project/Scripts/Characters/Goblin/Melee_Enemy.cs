using UnityEngine;

public class Melee_Enemy : WalkToPlayer
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!gameObject.GetComponent<Health>().IsDead)
        {
            Movement();
            Attack();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
            animator.SetBool("IsAttacking", false);
            Destroy(gameObject, 3f);
        }
    }
}
