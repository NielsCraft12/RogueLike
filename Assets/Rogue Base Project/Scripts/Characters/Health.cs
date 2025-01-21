using Sirenix.OdinInspector;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool IsDead { get; private set; }

    [Title("Health")]
    [SerializeField]
    protected int maxHealth = 100;

    [SerializeField]
    public int currentHealth;

    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            IsDead = true;
            animator.SetBool("IsDead", true);
            gameObject.layer = LayerMask.NameToLayer("Dead");
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
