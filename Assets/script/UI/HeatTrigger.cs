using UnityEngine;

public class HeatTrigger : MonoBehaviour
{
    //id the trigger and the number it exist
    public GameObject trigger;
    public Timer tm;

    public bool turOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            turOn = true;


            tm.countUp = true;
            tm.HeatingUp();




        }
    }
}
