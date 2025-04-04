using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{

    [SerializeField] WeaponSO weaponSO;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject zoomVignette;
    

    const string SHOOT_STRING = "Shoot";

    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    Animator animator;
    FirstPersonController firstPersonController;

    bool canShoot = true;
    float shootTimer = 0.0f;
    float defaultFOV, defaultRotationSpeed;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        defaultFOV = virtualCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    private void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        HandleShoot();
        HandleFireRate();
        HandleZoom();

    }

    public void SwitchWeapon(WeaponSO weaponPickedUp)
    {
        Debug.Log("Picked up " + weaponPickedUp.name);

        if (currentWeapon)
        { 
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponPickedUp.weaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.weaponSO = weaponPickedUp;
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

    void HandleZoom()
    {
        if (!weaponSO.CanZoom)
            return;

        if (starterAssetsInputs.zoom)
        {
            Debug.Log("Zooming in");

            virtualCamera.m_Lens.FieldOfView = weaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(weaponSO.ZoomRotationSpeed);

        }

        else
        {
            Debug.Log("Not Zooming");

            virtualCamera.m_Lens.FieldOfView = defaultFOV;
            zoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
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
