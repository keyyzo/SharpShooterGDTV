using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject enemyRobotPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float timeToSpawn = 5.0f;
    PlayerHealth player;


    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();

        SpawnRobot();
    }

    void SpawnRobot()
    {
        StartCoroutine(SpawnRobotRoutine());
    }

    IEnumerator SpawnRobotRoutine()
    {
        while (player) 
        {
            GameObject enemyRobotToSpawn = enemyRobotPrefab;

            Vector3 spawnPosition = spawnPoint.position;

            Instantiate(enemyRobotToSpawn, spawnPosition, transform.rotation);

            yield return new WaitForSeconds(timeToSpawn);

            

        }

       
    }
}
