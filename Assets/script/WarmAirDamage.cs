using UnityEngine;

public class WarmAirDamage : MonoBehaviour
{
    public int airIndex = 0;
    public bool lostWater = false;

    //id the warm air and the number it exist
    public GameObject warmAir;
    private int airAmount = 1;
    private Vector3 startPosition;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WarmAir")
        {
            lostWater = true;
            airIndex++;
            Debug.Log("Lost water "+  airIndex);
            Destroy(other.gameObject);

        }
    }




}
