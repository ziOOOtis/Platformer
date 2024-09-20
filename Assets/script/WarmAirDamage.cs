using UnityEngine;

public class WarmAirDamage : MonoBehaviour
{
    public EnemyZone ez;
    public bool lostWater = false;

    //id the warm air and the number it exist
    public GameObject warmAir;


    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            lostWater = true;

            // Notify the WaterSpawner to respawn the water after a delay
            ez.WaterLosted();
            other.GetComponent<PhysicsJump>().LostWater(this);

            // Destroy the water object
            Destroy(gameObject);

        }
    }




}
