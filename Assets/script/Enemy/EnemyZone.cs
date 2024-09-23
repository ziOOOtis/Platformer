using UnityEngine;
using System.Collections;

public class EnemyZone : MonoBehaviour
{
    public GameObject AirPrefab; // Reference to the water prefab
    public float respawnTime = 2f;  // Time to respawn the water after destruction

    private GameObject currentAir; // Holds reference to the current water object
    private Vector3 spawnPosition;   // Store the original spawn position

    void Start()
    {
        // Store the initial spawn position
        spawnPosition = transform.position ;

    }

    // This method spawns the water
    private void SpawnAir()
    {
        currentAir = Instantiate(AirPrefab, spawnPosition, Quaternion.identity);
        // Assign ourselves as the water spawner that belongs to this water
        currentAir.GetComponent<WarmAirDamage>().ez = this;

    }

    // This method is called when the water is collected/destroyed
    public void WaterLosted()
    {
        // Start the coroutine to respawn the water after a delay
        StartCoroutine(RespawnAir());
    }

    // Coroutine to wait for the respawn time before creating a new water object
    private IEnumerator RespawnAir()
    {
        // Wait for the specified respawn time (2 seconds)
        yield return new WaitForSeconds(respawnTime);

        // Spawn a new water object
        SpawnAir();
    }
}

