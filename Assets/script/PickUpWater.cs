

using UnityEngine;

public class PickUpWater : MonoBehaviour
{

    public WaterSpawner ws;
    [SerializeField]private bool isSingle;
    public bool getWater = false;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the water
        if (other.CompareTag("Player"))
        {

            getWater = true;


                // Notify the WaterSpawner to respawn the water after a delay
                ws.WaterCollected();
     

            other.GetComponent<PhysicsJump>().GetWater(this);

            // Destroy the water object
            Destroy(gameObject);

        }
    }
}

