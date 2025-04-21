using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject projectileHitVFX;
    [SerializeField] float projectileSpeed = 5.0f;
    int damageToPlayer = 2;

    //PlayerHealth player;
    Rigidbody rb;

    const string PLAYER_STRING = "Player";

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //player = FindFirstObjectByType<PlayerHealth>();

        Fire();
    }

    void Fire()
    {
        rb.linearVelocity = transform.forward * projectileSpeed;
    }

    public void Init(int damage)
    { 
        damageToPlayer = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag(PLAYER_STRING))
        //{
        //    PlayerHealth player = other.GetComponent<PlayerHealth>();
        //    player?.TakeDamage(damageToPlayer);

        //}

        //Instantiate(projectileHitVFX, transform.position, Quaternion.identity);
        //Destroy(this.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            player?.TakeDamage(damageToPlayer);

        }

        Instantiate(projectileHitVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }



}
