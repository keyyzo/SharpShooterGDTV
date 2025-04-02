using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{

    [SerializeField] WeaponSO weaponSO;
    

    const string SHOOT_STRING = "Shoot";

    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    Animator animator;

    bool canShoot = true;
    float shootTimer = 0.0f;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        HandleShoot();
        HandleFireRate();

    }

    public void SwitchWeapon(WeaponSO weaponPickedUp)
    {
        Debug.Log("Picked up " + weaponPickedUp.name);
    }

    private void HandleShoot()
    {
        if (!starterAssetsInputs.shoot)
            return;

        if (canShoot)
        {
            currentWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            canShoot = false;

            
        }

        if (!weaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }


       

    }

    void HandleFireRate()
    {
        if (!canShoot)
        {
            shootTimer += Time.deltaTime;
            Debug.Log("Chambering...");
        }
        

        if (shootTimer >= weaponSO.FireRate)
        {
            canShoot = true;
            shootTimer = 0.0f;
            Debug.Log("Can Shoot!");
        }
    }

    
}
