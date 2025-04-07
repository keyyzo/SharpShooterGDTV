using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{

    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] TMP_Text ammoText;

    const string SHOOT_STRING = "Shoot";

    WeaponSO currentWeaponSO;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    Animator animator;
    FirstPersonController firstPersonController;

    bool canShoot = true;
    float shootTimer = 0.0f;
    float defaultFOV, defaultRotationSpeed;
    int currentAmmo;

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
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    private void Update()
    {
        HandleShoot();
        HandleFireRate();
        HandleZoom();

    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        if (currentAmmo > currentWeaponSO.MagazineSize)
        { 
            currentAmmo = currentWeaponSO.MagazineSize;
        }

        ammoText.text = currentAmmo.ToString("D2");
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
        this.currentWeaponSO = weaponPickedUp;
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    private void HandleShoot()
    {
        if (!starterAssetsInputs.shoot)
            return;

        if (canShoot && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            canShoot = false;
            AdjustAmmo(-1);

            
        }

        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }


       

    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom)
            return;

        if (starterAssetsInputs.zoom)
        {
            Debug.Log("Zooming in");

            virtualCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);

        }

        else
        {
            Debug.Log("Not Zooming");

            virtualCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
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
        

        if (shootTimer >= currentWeaponSO.FireRate)
        {
            canShoot = true;
            shootTimer = 0.0f;
            Debug.Log("Can Shoot!");
        }
    }

    
}
