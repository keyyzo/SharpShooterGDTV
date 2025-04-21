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
    GameObject playerTarget;

    const string PLAYER_CAMERA_TARGET = "CinemachineTarget";

    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        playerTarget = GameObject.FindGameObjectWithTag(PLAYER_CAMERA_TARGET);

        FireProjectile();
    }

    private void Update()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    { 
        if(player && playerTarget)
            turretHead.LookAt(playerTarget.transform);
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
            if (playerTarget)
            {
                newProjectile.transform.LookAt(playerTarget.transform);
            }
            
            newProjectile.Init(damage);
        }
    }
}
