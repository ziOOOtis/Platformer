using UnityEngine;

public class HeatTrigger : MonoBehaviour
{
    //id the trigger and the number it exist
    public GameObject trigger;
    public Timer tm;
    public GameObject jumpScoreZone;

    [SerializeField] private BoxCollider bc;
    public TimeManager timeManager;//slower time
    [SerializeField] private Animator animator;


    public bool turOn;
    public bool haveUsed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        jumpScoreZone.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && haveUsed==false)
        {
            turOn = true;
            haveUsed=true;

            timeManager.DoSLowmotion(0.5f); //try to slow down.

            animator.SetBool("turnOn", true);
            


            tm.countUp = true;
            tm.HeatingUp();
            jumpScoreZone.gameObject.SetActive(true);



        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("turnOn", false);
            animator.SetBool("haveUsed", true);


        }
    }

}
