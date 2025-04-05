using UnityEngine;

public abstract class Pickup : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 100f;


    protected const string PLAYER_STRING = "Player";


    private void Update()
    {
        RotatePickup();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);
            Destroy(this.gameObject);

        }
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon);

    void RotatePickup()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

}
