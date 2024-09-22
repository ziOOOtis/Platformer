using UnityEngine;

public class JumpHeightScoring : MonoBehaviour
{
    public Transform player;  // Reference to the player object
    public HeatTrigger ht;
    public FryZone fz;
    public GameObject heightScore;


    public float highestY;    // Stores the highest Y position during jump
    private bool isInZone;     // To check if the player is in the scoring zone

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Player entered the scoring zone
            heightScore.gameObject.SetActive(true);

            isInZone = true;
            highestY = player.position.y; // Initialize with current height
            Debug.Log("Player enter");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isInZone && other.gameObject.tag == "Player")
        {
            // Continuously update the highest Y point during the jump
            if (player.position.y > highestY)
            {
                highestY = player.position.y;
                //Debug.Log("Player stay");
            }
        }
        if (fz.isCooking)
        {
            Debug.Log("Highest Jump Point: " + highestY);

            // Implement score calculation or end game logic here
            CalculateScore(highestY);
            fz.isCooking = false;

          
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Player leaves the zone, finalize the score based on highestY
            isInZone = false;
            Debug.Log("Highest Jump Point: " + highestY);

            // Implement score calculation or end game logic here
            CalculateScore(highestY);
        }
    }

    void CalculateScore(float height)
    {
        // Score or reward logic based on height
        // For example, higher height gives higher score:
        int score = Mathf.FloorToInt(height * 10); // Example scoring system
        Debug.Log("Final Score: " + score);
    }
}
