using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;

    [SerializeField] GameObject projectilePrefab;

    [SerializeField] float fireRate = 3.0f;

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
            Instantiate(projectilePrefab, projectileSpawnPoint.position, turretHead.rotation);
        }
    }
}
