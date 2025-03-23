using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int damagePerBullet = 1;
    [SerializeField] ParticleSystem muzzleFlash;

    StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    private void Update()
    {
        HandleShoot();

    }

    private void HandleShoot()
    {
        if (!starterAssetsInputs.shoot)
            return;

        muzzleFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(damagePerBullet);

            starterAssetsInputs.ShootInput(false);

            // Below was my implementation for the same solution
            // this works, however to keep consistent with the course I have commented it out

            //if (hit.collider.TryGetComponent(out EnemyHealth enemy))
            //{ 
            //    enemy.TakeDamage(damagePerBullet);
            //}
        }
    }
}
