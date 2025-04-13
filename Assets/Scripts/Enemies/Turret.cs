using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;

    [SerializeField] GameObject projectilePrefab;

    [SerializeField] float fireRate = 3.0f;
    [SerializeField] int damage = 2;

    PlayerHealth player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();

        FireProjectile();
    }

    private void Update()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    { 
        turretHead.LookAt(playerTargetPoint);
    }

    void FireProjectile()
    {
        StartCoroutine(FireProjectileRoutine());
    }

    IEnumerator FireProjectileRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(fireRate);
            Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(playerTargetPoint);
            newProjectile.Init(damage);
        }
    }
}
