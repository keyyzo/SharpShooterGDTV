using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask interactionLayers;

    public void Shoot(WeaponSO weaponSO)
    {
        
        muzzleFlash.Play();
       
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {


            Vector3 hitSpawnPos = hit.point;

            GameObject hitVFXGO = Instantiate(weaponSO.HitVFXPrefab, hitSpawnPos, Quaternion.identity);

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
