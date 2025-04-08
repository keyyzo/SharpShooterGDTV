using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 5;

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
            //Destroy(this.gameObject);
            Debug.Log("Player has died!");
        }
    }
}
