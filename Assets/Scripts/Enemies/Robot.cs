using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    
    FirstPersonController playerObject;

    NavMeshAgent agent;

    const string PLAYER_STRING = "Player";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = FindFirstObjectByType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerObject)
            return;

        agent.SetDestination(playerObject.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        { 
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            enemyHealth.SelfDestruct();
        }
    }
}
