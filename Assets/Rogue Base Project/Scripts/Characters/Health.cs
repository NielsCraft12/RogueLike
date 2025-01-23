using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    public bool IsDead { get; private set; }

    [Title("Health")]
    [SerializeField]
    public int maxHealth = 100;

    [SerializeField]
    public int currentHealth;

    protected Animator animator;

    [SerializeField]
    private List<GameObject> items = new List<GameObject>();

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

            if (gameObject.CompareTag("Player"))
            {
                GameObject.FindAnyObjectByType<MenuManager>().GetComponent<MenuManager>().GameOver();

            }

            if (gameObject.CompareTag("Enemy"))
            {
                GameObject.FindAnyObjectByType<WaveManager>().GetComponent<WaveManager>().currentEnemies--;
                int chance = Random.Range(1, 3);
                if (chance == 1)
                {
                    int randomIndex = Random.Range(0, items.Count);
                    Instantiate(items[randomIndex], transform.position, Quaternion.identity);
                }
            }


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
