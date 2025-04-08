using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField] int startingHealth = 5;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCameraTransform;
    [SerializeField] Image[] shieldBars;

    int currentHealth;
    int gameOverVirtualCameraPriority = 20;

    private void Awake()
    {
        currentHealth = startingHealth;

        AdjustHealthUI();
    }

    public void TakeDamage(int damageToTake)
    {
        currentHealth -= damageToTake;
        AdjustHealthUI();


        if (currentHealth <= 0)
        {
            weaponCameraTransform.parent = null;
            deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
            Destroy(this.gameObject);
            Debug.Log("Player has died!");
        }
    }

    void AdjustHealthUI()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shieldBars[i].gameObject.SetActive(true);
            }

            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }
}
