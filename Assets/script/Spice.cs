using UnityEngine;

public class Spice : MonoBehaviour
{
    public bool getSpice = false;
    public int spiceIndex;
    public GameObject player;
    public GameObject trigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the water
        if (other.CompareTag("Player"))
        {

            getSpice = true;
            spiceIndex++;
            GameManager.SetSpiceScore(spiceIndex);

            player.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);

            other.GetComponent<PhysicsJump>().GetSpice(this);

            Destroy(trigger);


        }
    }
}
