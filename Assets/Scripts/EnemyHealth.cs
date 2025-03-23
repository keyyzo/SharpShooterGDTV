using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;

    int currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageToTake)
    { 
        currentHealth -= damageToTake;

        if (currentHealth <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
