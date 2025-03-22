using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    
    FirstPersonController playerObject;

    NavMeshAgent agent;


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
        agent.SetDestination(playerObject.transform.position);
    }
}
