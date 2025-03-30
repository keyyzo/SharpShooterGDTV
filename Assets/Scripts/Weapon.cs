using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] int damagePerBullet = 1;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitVFXPrefab;
    [SerializeField] Animator animator;

    const string SHOOT_STRING = "Shoot";

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
        animator.Play(SHOOT_STRING, 0, 0f);
        starterAssetsInputs.ShootInput(false);

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {

            Vector3 hitSpawnPos = hit.point;

            GameObject hitVFXGO = Instantiate(hitVFXPrefab, hitSpawnPos, Quaternion.identity);

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);

            


            // Below was my implementation for the same solution
            // this works, however to keep consistent with the course I have commented it out

            //if (hit.collider.TryGetComponent(out EnemyHealth enemy))
            //{ 
            //    enemy.TakeDamage(damagePerBullet);
            //}
        }
    }
}
