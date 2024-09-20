using UnityEngine;
using System.Collections;

public class WaterSpawner : MonoBehaviour
{
    public GameObject waterPrefab; // Reference to the water prefab
    public float respawnTime = 2f;  // Time to respawn the water after destruction

    private GameObject currentWater; // Holds reference to the current water object
    private Vector3 spawnPosition;   // Store the original spawn position

    void Start()
    {
        // Store the initial spawn position
        spawnPosition = transform.position + Vector3.up * 0.15f;

        // Spawn the initial water object
        SpawnWater();
    }

    // This method spawns the water
    private void SpawnWater()
    {
        currentWater = Instantiate(waterPrefab, spawnPosition, Quaternion.identity);
        // Assign ourselves as the water spawner that belongs to this water
        currentWater.GetComponent<PickUpWater>().ws = this;
    }

    // This method is called when the water is collected/destroyed
    public void WaterCollected()
    {
        // Start the coroutine to respawn the water after a delay
        StartCoroutine(RespawnWater());
    }

    // Coroutine to wait for the respawn time before creating a new water object
    private IEnumerator RespawnWater()
    {
        // Wait for the specified respawn time (2 seconds)
        yield return new WaitForSeconds(respawnTime);

        // Spawn a new water object
        SpawnWater();
    }
}
