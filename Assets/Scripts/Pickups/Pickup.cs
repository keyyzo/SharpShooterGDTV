using TMPro;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float cooldownTimerLength = 60f;
    [SerializeField] string cooldownText = "Pickup available in: ";
    [SerializeField] TMP_Text cooldownTMP;

    [SerializeField] Material opaqueMat;
    [SerializeField] Material transparentMat;

    protected const string PLAYER_STRING = "Player";

    protected bool isOnCooldown = false;
    protected float cooldownTimer = 0.0f;

    Material material;
    MeshRenderer meshRenderer;
    GameObject playerObj;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        playerObj = GameObject.FindGameObjectWithTag(PLAYER_STRING);
    }

    private void Update()
    {
        RotatePickup();
        PointCooldownTextToPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING) && !isOnCooldown)
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);
            cooldownTMP.gameObject.SetActive(true);
            meshRenderer.material = transparentMat;

            cooldownTimer = cooldownTimerLength;
            isOnCooldown = true;

           // Destroy(this.gameObject);

        }
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon);

    void RotatePickup()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    void PointCooldownTextToPlayer()
    {
        if (isOnCooldown && cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownTMP.gameObject.transform.forward = Camera.main.transform.forward;
            cooldownTMP.text = cooldownText + cooldownTimer.ToString("F1");
        }

        else
        {
            isOnCooldown = false;
            cooldownTimer = 0.0f;
            meshRenderer.material = opaqueMat;
            cooldownTMP.gameObject.SetActive(false);
        }
    }

    
}
