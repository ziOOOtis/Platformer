/*using UnityEngine;


public class PickUpWater : MonoBehaviour
{

    public int waterIndex = 0;
    public bool getWater= false;

    public GameObject waterPrefab; // Reference to the water object prefab


    public bool waterExists = true; // Flag to check if water exists


    void Start()
    {

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            getWater = true;
            waterIndex++;
            waterExists = false;
            Debug.Log(waterIndex);

            FindFirstObjectByType<WaterSpawner>().WaterDestroyed();


        }
    }


}*/

using UnityEngine;

public class PickUpWater : MonoBehaviour
{

    [SerializeField] private WaterSpawner ws;
    public bool getWater = false;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the water
        if (other.CompareTag("Player"))
        {
            // Do whatever action you want when the player collects the water (e.g., increase water count)
            Debug.Log("Water Collected!");
            getWater = true;

            // Destroy the water object
            Destroy(gameObject);

            // Notify the WaterSpawner to respawn the water after a delay
            ws.WaterCollected();
        }
    }
}

