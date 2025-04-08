using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 1;

    const string PLAYER_STRING = "Player";

    private void Start()
    {
        Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        //TODO: Develop functionality to damage the player

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(PLAYER_STRING))
            { 
                PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();

                if (!playerHealth)
                    continue;

                playerHealth?.TakeDamage(damage);

                break;

            }
        }
    }
}
