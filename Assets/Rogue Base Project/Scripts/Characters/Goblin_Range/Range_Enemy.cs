using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class Range_Enemy : WalkToPlayer
{
    [SerializeField]
    [Required]
    GameObject Projectile;

    [SerializeField]
    [Required]
    GameObject ShootingPoint;

    protected override void Start()
    {
        base.Start();
    }

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

    public virtual void Throw()
    {
        GameObject projectile = Instantiate(
            Projectile,
            ShootingPoint.transform.position,
            quaternion.identity
        );
    }
}
