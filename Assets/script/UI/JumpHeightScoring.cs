using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class JumpHeightScoring : MonoBehaviour
{
    public Transform player;  // Reference to the player object
    public HeatTrigger ht;
    public FryZone fz;
    public GameObject heightScore;

    public TimeManager timeManager;//slower time

    float waitingTimes = 5f;



    public float highestY;    // Stores the highest Y position during jump
    private bool isInZone;     // To check if the player is in the scoring zone
    public int score;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Player entered the scoring zone
            heightScore.gameObject.SetActive(true);
            timeManager.DoSLowmotion(0.1f); //try to slow down.

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
            StartCoroutine(LoadToEnd());


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
        score = Mathf.FloorToInt(height * 10); // Example scoring system
        GameManager.SetHeightScore(score);

        Debug.Log("Final Score: " + score);
    }

    private IEnumerator LoadToEnd()
    {
        yield return new WaitForSeconds(waitingTimes);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }




}
